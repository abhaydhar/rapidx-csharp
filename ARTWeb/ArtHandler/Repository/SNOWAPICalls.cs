using ArtHandler.DAL;
using ArtHandler.Interface;
using ArtHandler.Model;
using ArtHandler.Snow;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Threading;

namespace ArtHandler.Repository
{
    public class SNOWAPICalls : Iitsmtool
    {
        private string APIResult = string.Empty;
        private string URL = string.Empty;
        private DataTable dt = null;
        private string UserName = string.Empty;
        private string Password = string.Empty;
        private string WorkGroup = string.Empty;
        private string Description = string.Empty;
        private string Category = string.Empty;
        private string business_service = string.Empty;
        private string CallerEmailID = string.Empty;
        private string WebMethod = string.Empty;
        private string ArtCategory = string.Empty;
        private string Urgency = string.Empty;
        private string Priority = string.Empty;
        private string Impact = string.Empty;

        private string Configuration_Item = string.Empty;
        private string Description_Unlock = string.Empty;
        private string Description_Change = string.Empty;
        private string Description_Forgot = string.Empty;
        private string Assigned_To = string.Empty;
        //int TicketNo = 0;
        private string UserEmailID = string.Empty;

        //string Category = Constants.PasswordResetCategory;
        private string Authorization = string.Empty;

        private string GSDWorkGroupName = string.Empty;

        public void GetSNOWInstanceDetails(string callType)
        {

            dt = new DAL_Settings().GetITSMToolInfo(Constants.Snow, callType);
            if (dt.Rows.Count > 0)
            {
                URL = Convert.ToString(dt.Rows[0]["URL"]);

                //if(callType == Constants.ITSMCreate)
                //    URL = "https://hexawareindev.service-now.com/api/now/v1/table/incident";
                //if (callType == Constants.ITSMUpdate)
                //    URL = "https://hexawareindev.service-now.com/api/now/v1/table/incident/{sysid}";


                UserName = Convert.ToString(dt.Rows[0]["Username"]);
                Password = Utility.Encryptor.Decrypt(Convert.ToString(dt.Rows[0]["Pwd"]), Constants.PASSPHARSE);
                WorkGroup = Convert.ToString(dt.Rows[0]["Workgroup"]);
                Description = Convert.ToString(dt.Rows[0]["Description"]);
                Category = Convert.ToString(dt.Rows[0]["InstanceCode"]);
                CallerEmailID = Convert.ToString(dt.Rows[0]["CallerEmailID"]);
                WebMethod = Convert.ToString(dt.Rows[0]["HTTPMethod"]);
                GSDWorkGroupName = Convert.ToString(dt.Rows[0]["GSDWorkGroupName"]);
                ArtCategory = Convert.ToString(dt.Rows[0]["Category"]);
                business_service = Convert.ToString(dt.Rows[0]["Service"]);

                Urgency = Convert.ToString(dt.Rows[0]["Urgency"]);
                Priority = Convert.ToString(dt.Rows[0]["Priority"]);
                Impact = Convert.ToString(dt.Rows[0]["Impact"]);

                Configuration_Item = Convert.ToString(dt.Rows[0]["Configuration_Item"]);
                Description_Unlock = Convert.ToString(dt.Rows[0]["Description_Unlock"]);
                Description_Change = Convert.ToString(dt.Rows[0]["Description_Change"]);
                Description_Forgot = Convert.ToString(dt.Rows[0]["Description_Forgot"]);
                Assigned_To = Convert.ToString(dt.Rows[0]["Assigned_to"]);
            }
        }

        public string CreateIncident(string userID, string userEmail, int status, string UserMessage, string GSDMessage, string category, string userActivity, string adPhysicalOfficeDelivery, ref string sys_id)
        {
            try
            {
                GetSNOWInstanceDetails(Constants.ITSMCreate);
                string result = string.Empty;

                SnowCreateTicket sct = new SnowCreateTicket();

                //sct.EventType = Constants.Incident;
                //sct.TicketID = string.Empty;
                sct.short_description = GSDMessage;
                UserEmailID = userEmail;
                if (!string.IsNullOrEmpty(UserEmailID))
                {
                    sct.caller_id = userID;
                }
                else
                {
                    sct.caller_id = CallerEmailID;
                }

                if (userActivity == Constants.CHANGE_PASSWORD)
                {
                    sct.description = Description_Change;
                }
                else if (userActivity == Constants.RESET_PASSWORD)
                {
                    sct.description = Description_Forgot;
                }
                else if (userActivity == Constants.UNLOCK_ACCOUNT)
                {
                    sct.description = Description_Unlock;
                }

                sct.urgency = SnowUrgency.Medium;
                sct.impact = SnowImpact.Low;
                //sct.cmdb_ci = Configuration_Item;
                sct.business_service = business_service;

                sct.location = GetUserDetails(userID, Constants.location, Constants.display_value);
                sct.u_building_name = GetUserDetails(userID, Constants.u_building_name);
                sct.u_project = GetUserDetails(userID, Constants.u_project);
                sct.u_contact_number = GetUserDetails(userID, Constants.mobile_phone);

                sct.u_service_category = Constants.ARTCATEGORY;
                sct.u_reference_2 = category;
                sct.state = SnowState.InProgress;
                sct.contact_type = Constants.SNOWContact_type;
                //sct.assigned_to = Assigned_To;
                sct.assignment_group = WorkGroup;
                //sct.u_support_level = "Level 2 Support";

                result = CallAPI(URL, JsonSerializer(sct), RequestMethod.POST);
                //SnowCreateResponse res = JsonDeSerializer<SnowCreateResponse>(result);

                JObject res = JObject.Parse(result);
                string ticketNum = res["result"]["number"].ToString();
                sys_id = res["result"]["sys_id"].ToString();
                
                LoggingRepository.ITSMTraceLog(Constants.ITSMSNOW, JsonSerializer(sct), result, ticketNum, "Snow CreateIncident");

                return ticketNum;

            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        /// <summary>
        /// call this method for incident creation
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="status"></param>
        /// <param name="ticketno"></param>
        public string CreateIncident(string userID, string userEmailID, string UserMessage, string GSDMessage, string category, string userActivity, string adPhysicalOfficeDelivery, ref string sys_id)
        {
            try
            {
                //UserEmailID = userEmailID;
                //Category = category;
                //return CreateIncident(userID, Constants.SummitNewStatus, 0, userID+" " +Description );
                return CreateIncident(userID, userEmailID, SnowState.New, UserMessage, GSDMessage, category, userActivity, adPhysicalOfficeDelivery, ref sys_id);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        /// <summary>
        /// post data to FS API
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public string CallAPI(string URL, string json, string method)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(URL);
                request.ContentType = RequestMethod.ApplicationJson;
                request.Method = method;
                //request.Headers["Authorization"] = Authorization;
                //UserName = method == RequestMethod.GET ? "Hexateam" : UserName;
                request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(UserName + ":" + Password));

                if (method != RequestMethod.GET)
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    APIResult = streamReader.ReadToEnd();
                }
            }
            catch (Exception exp)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, exp.Message.ToString(), exp.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
            }
            return APIResult;
        }

        public string JsonSerializer(dynamic obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public T JsonDeSerializer<T>(string json) where T : class, new()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }

        /// <summary>
        /// to update
        /// </summary>
        /// <param name="status"></param>
        /// <param name="ticketno"></param>
        /// <param name="Usrmessage"></param>
        /// <param name="GSDmessage"></param>
        /// <param name="reassign"></param>
        /// <param name="isfirstime"></param>
        /// <param name="sys_id"></param>
        /// <returns></returns>
        public string UpdateIncident(string status, string ticketno, string Usrmessage, string GSDmessage, bool reassign, bool isfirstime, string category, string sys_id)
        {
            GetSNOWInstanceDetails(Constants.ITSMUpdate);

            string result = string.Empty;

            SnowUpdateTicket sut = new SnowUpdateTicket();
            //sut.sys_id = sys_id;
            sut.state = Constants.SNOWInProgressStatus;
            //if (!string.IsNullOrEmpty(Usrmessage))
            //{
            //    sut.comments = Usrmessage;
            //}
            //else
            //{
            //    sut.comments = string.Empty;
            //}
            //if (!string.IsNullOrEmpty(GSDmessage))
            //{
            //    sut.work_notes = GSDmessage;
            //}
            //else
            //{
            //    sut.work_notes = string.Empty;
            //}

            URL = URL.Replace("{sysid}", sys_id);
            result = CallAPI(URL, JsonSerializer(sut), RequestMethod.PATCH);

            //SnowUpdateResponse res = JsonDeSerializer<SnowUpdateResponse>(result);

            JObject res = JObject.Parse(result);
            LoggingRepository.ITSMTraceLog(Constants.ITSMSNOW, JsonSerializer(sut), result, res["result"]["number"].ToString(), "Snow UpdateIncident");

            return res["result"]["sys_id"].ToString();

        }

        /// <summary>
        /// to update
        /// </summary>
        /// <param name="status"></param>
        /// <param name="ticketno"></param>
        /// <param name="Usrmessage"></param>
        /// <param name="GSDmessage"></param>
        /// <param name="reassign"></param>
        /// <param name="isfirstime"></param>
        /// <param name="sys_id"></param>
        /// <returns></returns>
        public string ResolveIncident(string status, string ticketno, string Usrmessage, string GSDmessage, bool reassign, bool isfirstime, string sys_id, bool iscreateResolve)
        {
            string result = string.Empty;
            string UpdateResult = string.Empty;
            string ticketNum = string.Empty;
            GetSNOWInstanceDetails(Constants.ITSMUpdate);
            //SnowResolveTicket sut = new SnowResolveTicket();

            ////sut.EventType = Constants.Incident;
            ////sut.TicketID = ticketno;
            //sut.close_notes = Usrmessage;
            //sut.comments = Usrmessage;
            //sut.state = SnowState.Resolved;
            ////sut.assigned_to = Assigned_To;
            //sut.close_code = SnowCloseCode.ResolvedSuccessfully;

            var res_obj = new
            {
                close_notes = Usrmessage,
                comments = Usrmessage,
                state = SnowState.Resolved,
                close_code = SnowCloseCode.ResolvedSuccessfully
            };

            UpdateResult = CallAPI(URL.Replace("{sysid}", sys_id), JsonSerializer(new { state = 2, u_response_checked = true, assigned_to= Assigned_To}), RequestMethod.PATCH);
            LoggingRepository.ITSMTraceLog(Constants.ITSMSNOW, JsonSerializer(new { state = 2, u_response_checked = true, assigned_to = Assigned_To }), UpdateResult, ticketNum, "Snow Partial Update");

            result = CallAPI(URL.Replace("{sysid}", sys_id), JsonSerializer(res_obj), RequestMethod.PATCH);

            JObject res = JObject.Parse(result);
            ticketNum = res["result"]["number"].ToString();


            //if (iscreateResolve)
            //{
            //    SnowCreateResolveResponse res = JsonDeSerializer<SnowCreateResolveResponse>(result);
            //    ticketNum = res.result.number;
            //}
            //else
            //{
            //    SnowUpdateResolveResponse res = JsonDeSerializer<SnowUpdateResolveResponse>(result);
            //    ticketNum = res.result.number;
            //}

            LoggingRepository.ITSMTraceLog(Constants.ITSMSNOW, JsonSerializer(res_obj), result, ticketNum, "Snow ResolveIncident");

            return ticketNum;
        }

        public string ReassignIncident(string ticketno, string Usrmessage, string GSDmessage, bool reassign, bool isfirstime, string category, string sys_id)
        {
            string result = string.Empty;
            SnowReassignTicket sut = new SnowReassignTicket();
            //sut.sys_id = sys_id;
            //sut.state = Constants.SNOWWORKINPROGRESS;

            //if (!string.IsNullOrEmpty(Usrmessage))
            //{
            //    sut.comments = Usrmessage;
            //}
            //else
            //{
            //    sut.comments = string.Empty;
            //}
            //if (!string.IsNullOrEmpty(GSDmessage))
            //{
            //    sut.work_notes = GSDmessage;
            //}
            //else
            //{
            //    sut.work_notes = string.Empty;
            //}
            sut.assignment_group = GSDWorkGroupName;

            GetSNOWInstanceDetails(Constants.ITSMUpdate);
            URL = URL.Replace("{sys_id}", sys_id);
            result = CallAPI(URL, JsonSerializer(sut), RequestMethod.PATCH);
            SnowUpdateResponse res = JsonDeSerializer<SnowUpdateResponse>(result);
            return res.result.sys_id;
        }

        /// <summary>
        /// call this method for incident update
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="status"></param>
        /// <param name="ticketno"></param>
        public string UpdateIncident(string ticketno, string usrMessage, string gsdMessage, string userEmailID, string category, bool isfirstime, string sys_id)
        {
            UserEmailID = userEmailID;
            Category = category;
            return UpdateIncident(Constants.SummitInProgressStatus, ticketno, usrMessage, gsdMessage, false, isfirstime, category, sys_id);
        }

        /// <summary>
        /// to update the ticket as Resolved
        /// </summary>
        /// <param name="ticketno"></param>
        /// <param name="usrMessage"></param>
        /// <param name="gsdMessage"></param>
        /// <param name="userEmailID"></param>
        /// <param name="sys_id"></param>
        /// <returns></returns>
        public string ResolveIncident(string ticketno, string usrMessage, string gsdMessage, string userEmailID, string category, string sys_id, bool iscreateResolve)
        {
            UserEmailID = userEmailID;
            if (!string.IsNullOrEmpty(ticketno))
            {
                return ResolveIncident(Constants.SummitResolvedStatus, ticketno, usrMessage, gsdMessage, false, false, sys_id, iscreateResolve);
            }
            return ticketno;
        }

        /// <summary>
        /// to update the ticket as Reassigned
        /// </summary>
        /// <param name="ticketno"></param>
        /// <param name="usrMessage"></param>
        /// <param name="gsdMessage"></param>
        /// <param name="userEmailID"></param>
        /// <param name="category"></param>
        /// <param name="sys_id"></param>
        /// <returns></returns>
        public string ReassignIncident(string ticketno, string usrMessage, string gsdMessage, string userEmailID, string category, string sys_id)
        {
            Category = category;
            if (!string.IsNullOrEmpty(ticketno))
            {
                UserEmailID = userEmailID;
                return ReassignIncident(ticketno, usrMessage, gsdMessage, true, false, category, sys_id);
            }
            return ticketno;
        }

        public string UpdateIncidentDetails(string status, bool reassign, string ticketno, string message, string userEmailID, string category, string sys_id)
        {
            UpdateIncident(ticketno, message, message, userEmailID, category, true, sys_id);

            return ticketno;
            //throw new NotImplementedException();
        }

        public string GetUserDetails(string userId, string key, string subkey = "")
        {
            string SnowGetUserApi = ConfigurationManager.AppSettings["snowgetuserapi"].ToString();
            string result = string.Empty;
            string json = string.Empty;

            try
            {
                string SnowUrl = URL.Replace("incident", string.Format(SnowGetUserApi, "&", "&", userId));
                result = CallAPI(SnowUrl, json, RequestMethod.GET);

                LoggingRepository.ITSMTraceLog(Constants.ITSMSNOW, json, result, "Ticket Creation", "Snow user Information");

                if (!string.IsNullOrEmpty(result))
                {
                    JObject jsonObject = JObject.Parse(result);
                    if (!String.IsNullOrEmpty(subkey))
                        return jsonObject["result"][0][key][subkey].ToString();
                    else
                        return jsonObject["result"][0][key].ToString();
                }
            }
            catch (Exception ex)
            {

            }

            return string.Empty;

        }
    }
}