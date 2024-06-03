using ArtHandler.Model;
using ArtHandler.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.DAL
{
    public class DAL_Settings
    {
        public List<SettingsModel> GetSetting(string settingName)
        {
            try
            {
                DataSet dsSettings = new DataSet();
                List<SettingsModel> lstSettings = new List<SettingsModel>();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETSETTINGS, con);

                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("settingName", settingName));

                    adapter.Fill(dsSettings);
                }

                foreach (DataRow item in dsSettings.Tables[0].Rows)
                {
                    SettingsModel objSetting = new SettingsModel();
                    objSetting.setting_id = Convert.ToInt32(item["setting_id"]);
                    objSetting.setting_name = item[("setting_name")].ToString();
                    objSetting.setting_value = item["setting_value"].ToString();
                    objSetting.setting_date = Convert.ToDateTime(item["setting_date"].ToString());

                    lstSettings.Add(objSetting);
                }

                Singleton clientsessionidinstance = Singleton.Instance;
                clientsessionidinstance.ClientSessionID = new ApplicationSettingsModel()
                {
                    Is_Multilingual_Enabled = "Y",
                    Default_Lang = "Y",
                    Is_OTP_Enabled = "Y",
                    Total_Number_Of_Question_To_Register = "Y",
                    Total_Number_Of_Question_To_Validate = "Y",
                    Total_Number_Of_Questions = "Y",
                };


                return lstSettings;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        public ApplicationSettingsModel GetApplicationSettings()
        {
            try
            {
                DataSet dsSettings = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETSETTINGS, con);

                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("settingName", string.Empty));

                    adapter.Fill(dsSettings);
                }
                ApplicationSettingsModel objApplicationSet = new ApplicationSettingsModel();

                foreach (PropertyInfo p in typeof(ApplicationSettingsModel).GetProperties())
                {
                    string propertyName = p.Name;
                    DataRow[] dr = dsSettings.Tables[0].Select("setting_name = '" + propertyName + "'");

                    if (dr.Length > 0)
                    {
                        PropertyInfo propertyInfo = objApplicationSet.GetType().GetProperty(propertyName);
                        string settingValue = string.Empty;

                        if (propertyName == Constants.LDAPNETWORKUSERPASS || propertyName == Constants.NETWORKPASSWORD)
                        {
                            settingValue = Utility.Encryptor.Decrypt(dr[0]["setting_value"].ToString(), Constants.PASSPHARSE);
                        }
                        else
                        {
                            settingValue = dr[0]["setting_value"].ToString();
                        }

                        propertyInfo.SetValue(objApplicationSet, Convert.ChangeType(settingValue, propertyInfo.PropertyType), null);
                    }
                }

                return objApplicationSet;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        public List<OptionsModel> GetOptions()
        {
            try
            {
                DataSet dsOption = new DataSet();
                List<OptionsModel> lstOptions = new List<OptionsModel>();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETOPTIONS, con);
                    adapter.Fill(dsOption);
                }

                foreach (DataRow item in dsOption.Tables[0].Rows)
                {
                    OptionsModel objOptions = new OptionsModel();
                    objOptions.option_id = Convert.ToInt32(item["option_id"]);
                    objOptions.option_name = item[("option_name")].ToString();
                    objOptions.option_url = item["option_url"].ToString();
                    objOptions.isEnabled = Convert.ToBoolean(item["isEnabled"].ToString());

                    lstOptions.Add(objOptions);
                }

                return lstOptions;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        public List<LanguageModel> Getlanguages()
        {
            try
            {
                DataSet dsLang = new DataSet();
                List<LanguageModel> lstLang = new List<LanguageModel>();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETLANGUAGES, con);
                    adapter.Fill(dsLang);
                }

                foreach (DataRow item in dsLang.Tables[0].Rows)
                {
                    LanguageModel objLang = new LanguageModel();
                    objLang.lang_id = Convert.ToInt32(item["lang_id"]);
                    objLang.lang_name = item[("lang_name")].ToString();
                    objLang.lang_culture_name = item["Lang_Culture_Name"].ToString();
                    objLang.isEnabled = Convert.ToBoolean(item["isEnabled"].ToString());

                    lstLang.Add(objLang);
                }

                return lstLang;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        public DataTable GetSMSGateway(string provider)
        {
            try
            {
                DataSet dsOption = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETSMSGATEWAY, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("serviceprovider", provider));
                    adapter.Fill(dsOption);
                }
                return dsOption.Tables[0];
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        public DataTable GetITSMToolInfo(string providerName, string function)
        {
            try
            {
                DataSet dsOption = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETITSMTOOLINFO, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("itsm_provider", providerName));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("itsm_function", function));
                    adapter.Fill(dsOption);
                }

                return dsOption.Tables[0];
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        /// <summary>
        /// To Get the country Code details
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public DataTable GetCountryTelephoneCodes(string countryCode)
        {
            try
            {
                DataSet dsCodes = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETCOUNTRYTELEPHONECODES, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("country_Code", countryCode));
                    adapter.Fill(dsCodes);
                }

                return dsCodes.Tables[0];
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        /// <summary>
        /// To Get the Password Settings information
        /// </summary>
        /// <returns></returns>
        public List<PasswordSettingsModel> GetPasswordSettings()
        {
            try
            {
                DataSet dsPasswordSet = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETPASSWORDSETTINGS, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.Fill(dsPasswordSet);
                }

                List<PasswordSettingsModel> convertedList = (from rw in dsPasswordSet.Tables[0].AsEnumerable()
                                                             select new PasswordSettingsModel()
                                                             {
                                                                 Id = Convert.ToInt32(rw["Id"]),
                                                                 MinLength = Convert.ToInt32(rw["MinLength"]),
                                                                 MaxLength = Convert.ToInt32(rw["MaxLength"]),
                                                                 NumericLength = Convert.ToInt32(rw["NumericLength"]),
                                                                 SmallLength = Convert.ToInt32(rw["SmallLength"]),
                                                                 CapsLength = Convert.ToInt32(rw["CapsLength"]),
                                                                 AllowedSplChars = Convert.ToString(rw["AllowedSplChars"]),
                                                                 SplCharsLength = Convert.ToInt32(rw["SplCharsLength"]),
                                                                 CreatedDate = Convert.ToString(rw["CreatedDate"]),
                                                                 IsActive = Convert.ToInt32(rw["IsActive"]),
                                                             }).ToList();


                return convertedList;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        /// <summary>
        /// To Get the SMTP Details
        /// </summary>
        /// <returns></returns>
        public DataTable GetArtSMTP()
        {
            try
            {
                DataSet dsSMTP = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETARTSMTP, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.Fill(dsSMTP);
                }

                return dsSMTP.Tables[0];
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// To Get the LDAP settings
        /// </summary>
        /// <returns></returns>
        public List<LdapSettingsModel> GetArtLdapSettings()
        {
            try
            {
                DataSet dsLdap = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETARTLDAPSETTINGS, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.Fill(dsLdap);
                }

                List<LdapSettingsModel> convertedList = (from rw in dsLdap.Tables[0].AsEnumerable()
                                                         select new LdapSettingsModel()
                                                             {
                                                                 LdapConnectionPath = "LDAP://" + Convert.ToString(rw["Hostname"]),
                                                                 DomainName = Convert.ToString(rw["TopLevelHost"]),
                                                                 DomainExtn = Convert.ToString(rw["DomainExtn"]),
                                                                 LdapConnectionContainer = "LDAP://" + Convert.ToString(rw["Hostname"]),
                                                                 LdapnetworkUsername = Convert.ToString(rw["Username"]),
                                                                 LdapNetworkUserPass = Utility.Encryptor.Decrypt(Convert.ToString(rw["Pwd"]), Constants.PASSPHARSE),
                                                             }).ToList();


                return convertedList;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        /// <summary>
        /// To Get the validation db context like view name , column name to validate the DOB
        /// </summary>
        /// <returns></returns>
        public List<ArtValidationDBContext> GetArtValidationDBContext()
        {
            try
            {
                DataSet dsLdap = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETARTDBVALIDATIONCONTEXT, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.Fill(dsLdap);
                }

                List<ArtValidationDBContext> convertedList = (from rw in dsLdap.Tables[0].AsEnumerable()
                                                              select new ArtValidationDBContext()
                                                             {
                                                                 SColumnName = Convert.ToString(rw["SColumnName"]),
                                                                 ViewName = Convert.ToString(rw["ViewName"]),
                                                                 CColumnName = Convert.ToString(rw["CColumnName"]),
                                                             }).ToList();


                return convertedList;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public List<ExcemptedOUs> GetExcemptedOUs()
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETEXCEMPTEDOUS, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.Fill(ds);
                }

                List<ExcemptedOUs> lstExcemptedOUs = (from rw in ds.Tables[0].AsEnumerable()
                                                      select new ExcemptedOUs()
                                                                {
                                                                    OUPath = Convert.ToString(rw["OUPath"]),
                                                                    OUName = Convert.ToString(rw["OUName"]),
                                                                    IsActive = Convert.ToInt32(rw["IsActive"]),
                                                                    CreatedDate = Convert.ToDateTime(rw["CreatedDate"]),
                                                                    ModifiedDate = Convert.ToDateTime(rw["ModifiedDate"]),
                                                                }).ToList();


                return lstExcemptedOUs;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public List<CountryCodeModel> GetAllCountryCodeDetails()
        {
            try
            {
                DataSet dsContryCodes = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETALLCOUNTRYCODEDETAILS, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.Fill(dsContryCodes);
                }

                List<CountryCodeModel> lstCountryCodes = (from rw in dsContryCodes.Tables[0].AsEnumerable()
                                                          select new CountryCodeModel()
                                                                {
                                                                    Name = Convert.ToString(rw["Name"]),
                                                                    CountryCode = Convert.ToString(rw["CountryCode"]),
                                                                    TelephoneCode = Convert.ToString(rw["TelephoneCode"]),
                                                                    MobileNumberLength = Convert.ToString(rw["MobileNumberLength"]),
                                                                }).ToList();


                return lstCountryCodes;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
    }
}
