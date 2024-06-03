using ArtHandler.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Repository
{
    public class ADResetPassword
    {
        public bool ResetPassword(string userId, string password, ref string messgae)
        {
            string DomainName = SingletonLDAPSettings.Instance.LDAPSettings.DomainName;
            string DomainExtn = SingletonLDAPSettings.Instance.LDAPSettings.DomainExtn;
            string LdapPath = SingletonLDAPSettings.Instance.LDAPSettings.LdapConnectionPath;
            string NetUsername = SingletonLDAPSettings.Instance.LDAPSettings.LdapnetworkUsername;
            string NetUserCred = SingletonLDAPSettings.Instance.LDAPSettings.LdapNetworkUserPass;

            try
            {
                messgae = string.Empty;

                string dn = string.Empty;
                DirectoryEntry directoryEntry = new DirectoryEntry(LdapPath, DomainName + "\\" + NetUsername, NetUserCred);
                DirectorySearcher search = new DirectorySearcher(directoryEntry);
                search.Filter = "(SAMAccountName=" + userId + ")";
                SearchResult result = search.FindOne();
                if (result != null)
                {
                    dn = result.Properties["distinguishedName"][0].ToString();
                }

                /* initialize LdapConnection which inherites from DirectoryConnection  -
                 * DirectoryConnection cannot be initialized passing a directory to connect to */

                LdapDirectoryIdentifier lid = new LdapDirectoryIdentifier(DomainName + "." + DomainExtn + ":636");
                System.Net.NetworkCredential cred = new System.Net.NetworkCredential(NetUsername, NetUserCred, DomainName);

                using (LdapConnection ldapCon = new LdapConnection(lid))
                {
                    ldapCon.SessionOptions.ProtocolVersion = 3;
                    ldapCon.SessionOptions.VerifyServerCertificate =
                            new VerifyServerCertificateCallback((con, cer) => true);
                    ldapCon.Bind(cred);

                    if (ldapCon.SessionOptions.ProtocolVersion == 3)
                    {
                        // reset pwd utilizing pwd history
                        bool ispwdSet = PasswordChanger(ldapCon,
                                        dn, 
                                        ref messgae,
                                        pwdSet: password,
                                        enforceHistory: true);

                        if (ispwdSet)
                        {
                            DirectoryEntry userEntry = result.GetDirectoryEntry();

                            userEntry.Properties["pwdLastSet"][0] = -1;
                            userEntry.Properties["LockOutTime"].Value = 0; //unlock account
                            userEntry.CommitChanges();
                            userEntry.Close();
                            userEntry.Dispose();
                        }

                        directoryEntry.Close();
                        directoryEntry.Dispose();

                        return ispwdSet;
                    }
                    else
                        return false;
                }

            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }

        }
        private bool PasswordChanger(LdapConnection ldapCon,
                                            string distinguishedName,
                                            ref string messgae,
                                            string pwdDepricate = null,
                                            string pwdSet = null,
                                            bool enforceHistory = false,
                                            bool useOldOID = false
                                            )
        {
            bool letsgo = false;
            messgae = string.Empty;

            // the 'unicodePWD' attribute is used to handle pwd handling requests
            string attribute = "unicodePwd";

            // our modification control
            DirectoryAttributeModification[] damList = null;

            // the modifiy request
            ModifyRequest mrCall = null;

            //do we have an old and a new pwd -> change pwd
            if (!String.IsNullOrEmpty(pwdDepricate) && !String.IsNullOrEmpty(pwdSet))
            {
                // modification control for the delete operation
                DirectoryAttributeModification damDelete = new DirectoryAttributeModification();

                // attribute to handle
                damDelete.Name = attribute;

                // value to be send with the request
                damDelete.Add(BuildBytePWD(pwdDepricate));

                // this is a delete operation
                damDelete.Operation = DirectoryAttributeOperation.Delete;

                // modification control for the add operation
                DirectoryAttributeModification damAdd = new DirectoryAttributeModification();

                // attribute to handle
                damAdd.Name = attribute;

                // value to be send with the request
                damAdd.Add(BuildBytePWD(pwdSet));

                // this is an add operation
                damAdd.Operation = DirectoryAttributeOperation.Add;

                // combine modification controls
                damList = new DirectoryAttributeModification[] { damDelete, damAdd };

                // init modify request
                mrCall = new ModifyRequest(distinguishedName, damList);

                // we do have something to handle
                letsgo = true;
            }

            //do we have a pwd to set -> set pwd
            else if (!String.IsNullOrEmpty(pwdSet))
            {
                // modification control for the replace operation
                DirectoryAttributeModification damReplace = new DirectoryAttributeModification();

                // attribute to handle
                damReplace.Name = attribute;

                // value to be send with the request
                damReplace.Add(BuildBytePWD(pwdSet));

                // this is a replace operation
                damReplace.Operation = DirectoryAttributeOperation.Replace;

                // combine modification controls
                damList = new DirectoryAttributeModification[] { damReplace };

                // init modify request
                mrCall = new ModifyRequest(distinguishedName, damList);

                // should we utilize pwd history on the pwd reset?
                if (enforceHistory)
                {
                    // the actual extended control OID                     
                    string LDAP_SERVER_POLICY_HINTS_OID = "1.2.840.113556.1.4.2066";

                    //string LDAP_SERVER_POLICY_HINTS_OID = useOldOID ? "1.2.840.113556.1.4.2066" :
                    //                                                  "1.2.840.113556.1.4.2239";

                    // build value utilizing berconverter
                    byte[] value = BerConverter.Encode("{i}", new object[] { 1 });

                    // init extended control
                    DirectoryControl pwdHistory = new DirectoryControl(LDAP_SERVER_POLICY_HINTS_OID,
                                                                       value, false, true);

                    // add extended control to modify request
                    mrCall.Controls.Add(pwdHistory);
                }

                // we do have something to handle
                letsgo = true;
            }

            // something to be handled?
            if (letsgo)
            {
                DirectoryResponse drResult = null;

                string msg = "";

                try
                {
                    /* send the request into the DirectoryConnection
                     * and receive the response */
                    drResult = ldapCon.SendRequest(mrCall);

                    // display result code
                    msg = TranslateEx(drResult, null, distinguishedName);

                    return true;
                }

                catch (DirectoryOperationException doex)
                {
                    msg = TranslateEx(drResult, doex, distinguishedName);
                    messgae = msg;
                    return false;
                }

                catch (Exception ex)
                {
                    msg = TranslateEx(drResult, ex, distinguishedName);
                    messgae = msg;
                    return false;
                }
            }
            else
                return false;

        }

        /// <summary>
        /// build byte array from string pwd
        /// </summary>
        /// <param name="pwd">pwd string</param>
        /// <returns>byte array</returns>
        private static byte[] BuildBytePWD(string pwd)
        {

            return (Encoding.Unicode.GetBytes(String.Format("\"{0}\"", pwd)));
        }

        /// <summary>
        /// decode exception thrown
        /// </summary>
        /// <param name="dr">Directoryresponse from the SendRequest call</param>
        /// <param name="ex">the exception to decode</param>
        /// <param name="dn">the distinguishedName of the object we touched</param>
        /// <returns></returns>
        private static string TranslateEx(DirectoryResponse dr, Exception ex, string dn)
        {
            string ret = "";

            bool success = false;

            if (dr != null)
            { success = (dr.ResultCode == ResultCode.Success) ? true : false; }

            if (success)
            {
                ret = String.Format("Update pwd result: {0} \n\tfor {1}\n",
                                    dr.ResultCode.ToString(), dn);
            }

            else if (!success && (ex != null))
            {
                if (ex is DirectoryOperationException)
                {

                    DirectoryOperationException doex = (DirectoryOperationException)ex;

                    ret = String.Format("Update pwd result: {0} \n\tfor {1}\n",
                                        doex.Response.ResultCode.ToString(), dn);

                    string hex = doex.Response.ErrorMessage.Split(new char[] { ':' })[0];

                    int lex = 0;

                    if (int.TryParse(hex, System.Globalization.NumberStyles.HexNumber, null, out lex))
                    {
                        try
                        {
                            Win32Exception wex = new Win32Exception(lex);

                            ret = ret + String.Format("{0} ({1}) [{2}]\n",
                                                      wex.Message, doex.Response.ErrorMessage, doex.Message);
                        }

                        catch
                        {
                            ret = ret + String.Format("{0} [{1}]\n",
                                                      doex.Response.ErrorMessage, doex.Message);
                        }
                    }

                    else
                    {
                        ret = ret + String.Format("{0} [{1}]\n",
                                                  doex.Response.ErrorMessage, doex.Message);
                    }
                }

                else
                {
                    ret = String.Format("Update pwd result: Error \n\tfor {0}\n", dn);

                    ret = ret + String.Format("{0}\n", ex.Message);
                }
            }

            return ret;
        }
    }

}
