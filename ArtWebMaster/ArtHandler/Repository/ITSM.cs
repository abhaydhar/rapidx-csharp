﻿using ArtHandler.Interface;
using ArtHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Repository
{
    public class ITSM
    {
        public Iitsmtool GetITSMInstance(ref string itsm)
        {
            Iitsmtool objItsm;
            string itsmProvider = Singleton.Instance.ClientSessionID.ITSM_Provider_Name.ToUpper();

            switch (itsmProvider)
            {
                case "SNOW":
                    itsm = "SNOW";
                    objItsm = new SNOWAPICalls();
                    break;
                case "SUMMIT":
                    itsm = "SUMMIT";
                    objItsm = new Summit();
                    break;
                default:
                    itsm = "SNOW";
                    objItsm = new SNOWAPICalls();
                    break;
            }

            return objItsm;
        }


        //    public bool CreateIncident(string userId, string userActivity, string email, string description, string shortdescription, string category, string gsdEmail)
        //    {
        //        try
        //        {
        //            string itsmresult = string.Empty;
        //            string itsmProvider = string.Empty;
        //            string sysid = string.Empty;
        //            IncidentRepository objIncRepo = new IncidentRepository();
        //            int incidentId = 0;

        //            incidentId = objIncRepo.GetIncidentNo(userId, userActivity);

        //            Iitsmtool objItsm = GetITSMInstance(ref itsmProvider);

        //            if (incidentId == 0)
        //            {
        //                Log.LogTrace(new CustomTrace(userId, userActivity, "Create " + itsmProvider + " ITSM incident - START"));

        //                // Create a incident with resolved status in SNOW ITSM
        //                itsmresult = objItsm.CreateIncident(userId, email, description, shortdescription, category, description, ref sysid);

        //                //log itsm ticket no
        //                Log.LogITSM(userId, userActivity, itsmProvider, itsmresult);

        //                //log trace
        //                Log.LogTrace(new CustomTrace(userId, userActivity, "Create " + itsmProvider + " ITSM incident - END"));

        //            }
        //            else
        //            {
        //                Log.LogTrace(new CustomTrace(userId, userActivity, "Update " + itsmProvider + " ITSM incident - " + Convert.ToString(incidentId)));

        //                itsmresult = objItsm.UpdateIncident(Convert.ToString(incidentId), description, shortdescription, email, category, sysid);
        //            }

        //            if (!string.IsNullOrEmpty(itsmresult))
        //            {
        //                Log.LogTrace(new CustomTrace(userId, userActivity, "Resolved " + itsmProvider + " ITSM incident - START"));

        //                string resolveResult = objItsm.ResolveIncident(itsmresult, description, shortdescription, email, category, gsdEmail, sysid);

        //                Log.LogTrace(new CustomTrace(userId, userActivity, "Resolved " + itsmProvider + " ITSM incident - END"));
        //            }

        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
        //            return false;
        //        }
        //    }
        //}

        public bool CreateIncident(string userId, string userActivity, string email, string description, string shortdescription, string category, string gsdemail, string gsduserName)
        {
            try
            {
                string itsmresult = string.Empty;
                string itsmProvider = string.Empty;
                string sysid = string.Empty;
                string adPhysicalOfficeDelivery = string.Empty;
                bool iscreateResolve = false;

                IncidentRepository objIncRepo = new IncidentRepository();
                string incidentId = string.Empty;

                if (Singleton.Instance.ClientSessionID.Create_Incident_For_All != "Y")
                {
                    incidentId = objIncRepo.GetIncidentNo(userId, userActivity);
                }
                Iitsmtool objItsm = new ITSM().GetITSMInstance(ref itsmProvider);

                if (incidentId == "")
                {
                    iscreateResolve = true;
                    Log.LogTrace(new CustomTrace(userId, userActivity, "Create " + itsmProvider + " ITSM incident - START"));

                    // Create a incident with resolved status in SNOW ITSM
                    itsmresult = objItsm.CreateIncident(userId, email, description, shortdescription, category, userActivity, adPhysicalOfficeDelivery, ref sysid, gsduserName);

                    //log itsm ticket no
                    Log.LogITSM(userId, userActivity, itsmProvider, itsmresult, sysid);

                    //log trace
                    Log.LogTrace(new CustomTrace(userId, userActivity, "Create " + itsmProvider + " ITSM incident - END"));

                }
                else
                {
                    Log.LogTrace(new CustomTrace(userId, userActivity, "Update " + itsmProvider + " ITSM incident - " + Convert.ToString(incidentId)));

                    sysid = objIncRepo.GetSysNo(userId, userActivity);

                    itsmresult = objItsm.UpdateIncidentDetails(Constants.SummitAssignedStatus, true, Convert.ToString(incidentId), description, email, category, sysid);
                }

                if (!string.IsNullOrEmpty(itsmresult))
                {

                    string itsmresult2 = objItsm.UpdateIncidentState(Constants.INPROGRESS, Convert.ToString(incidentId), category, sysid);

                    Log.LogTrace(new CustomTrace(userId, userActivity, "Resolved " + itsmProvider + " ITSM incident - START"));

                    string resolveResult = objItsm.ResolveIncident(itsmresult, description, description, email, category, gsduserName, gsdemail, sysid, iscreateResolve);

                    Log.LogTrace(new CustomTrace(userId, userActivity, "Resolved " + itsmProvider + " ITSM incident - END"));
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
    }
}
