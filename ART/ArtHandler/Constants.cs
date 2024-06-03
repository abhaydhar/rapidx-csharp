using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler
{
    public static class Constants
    {
        public const string SP_USERQUESTIONANSWER = "Sp_UserQuestionAnswer";
        public const string SP_INSERTUSERQUESTIONANSWER = "Sp_InsertUserQuestionAnswer";
        public const string SP_GETQUESTION = "Sp_GetQuestion";
        public const string SP_DELETEUSERQUESTIONANSWER = "Sp_DeleteUserQuestionAnswer";
        public const string SP_GETQUESTIONFORUSERTOANSWER = "Sp_GetQuestionForUserToAnswer";
        public const string SP_GETSETTINGS = "Sp_GetSettings";
        public const string SP_GETOPTIONS = "Sp_GetOptions";
        public const string SP_GETLANGUAGES = "Sp_Getlanguages";
        public const string SP_CHECKACCOUNTLOCK = "Sp_CheckAccountLock";
        public const string SP_INSERTARTACCOUNTLOCK = "Sp_InsertArtAccountLock";
        public const string SP_ARTUSERREGISTEROTP = "SP_ArtUserRegisterOtp";
        public const string SP_ARTUSERREGISTER = "SP_ArtUserRegister";
        public const string SP_ArtUpdateEnrollLink = "SP_ArtUpdateEnrollLink";
        public const string SP_CHECKUSEROTPENABLED = "SP_CheckUserOTPEnabled";
        public const string SP_CHECKUSERREGISTERED = "Sp_CheckUserRegistered";
        public const string Sp_CheckEventSource = "Sp_CheckEventSource";

        public const string SP_GETSMSGATEWAY = "Sp_GetSMSGateWay";
        public const string SP_INSERTUSEROTP = "Sp_InsertUserOtp";
        public const string SP_VALIDATEUSEROTP = "Sp_ValidateUserOtp";
        public const string SP_VALIDATEENROLLOTP = "Sp_ValidateENROLLOtp";
        public const string SP_GETITSMTOOLINFO = "Sp_GetItsmToolInfo";
        public const string SP_GETQUESTIONCATEGORY = "Sp_GetQuestionCategory";
        public const string SP_INSERTCHANGEPASSWORDLOG = "Sp_InsertChangePasswordLog";
        public const string SP_INSERTUSERACTIVITYLOG = "Sp_InsertUserActivityLog";
        public const string SP_UPDATEUSERACTIVITYLOG = "Sp_UpdateUserActivityLog";
        public const string SP_INSERTARTUSERSMSSENT = "Sp_InsertArtUserSMSSent";
        public const string SP_DELETEUSERACCOUNTLOCKLOGS = "Sp_DeleteUserAccountLockLogs";
        public const string SP_INSERTARTREQUESTORINFO = "Sp_InsertArtRequestorInfo";
        public const string SP_GETCOUNTRYTELEPHONECODES = "Sp_GetCountryTelephoneCodes";
        public const string SP_GETPASSWORDSETTINGS = "Sp_GetPasswordSettings";
        public const string SP_INSERTARTUSEROTPATTEMPTINFO = "Sp_Insertartuserotpattemptinfo";
        public const string SP_RESETARTUSEROTPATTEMPTS = "Sp_ResetArtUserOtpAttempts";
        public const string SP_CHECKUSEROTPATTEMPTEXCEED = "Sp_CheckUserOtpAttemptExceed";
        public const string SP_CHECKUNUSEDOTPEXIST = "Sp_CheckUnUsedOTPExist";
        public const string Sp_GetUserEnrollmentKey = "Sp_GetUserEnrollmentKey";
        public const string SP_GETARTSMTP = "Sp_GetArtSMTP";
        public const string SP_GETARTLDAPSETTINGS = "Sp_GetArtLdapSettings";
        public const string SP_GETARTDBVALIDATIONCONTEXT = "Sp_GetArtDBvalidationContext";
        public const string SP_GETUSERREGISTEREDQUESTIONS = "Sp_GetUserRegisteredQuestions";
        public const string SP_GETALLCOUNTRYCODEDETAILS = "Sp_GetAllCountryCodeDetails";
        public const string SP_INSERTARTITSMLOG = "SP_InsertArtITSMLog";
        public const string SP_UPDATEUSERINFO = "SP_UpdateUserInfo";
        public const string SP_GETUSERINFO = "SP_GetUserInfo";
        public const string SP_INSERTARTITSMTRACE = "Sp_InsertArtItsmTrace";
        public const string SP_GETEXCEMPTEDOUS = "Sp_GetExcemptedOUs";
        public const string SP_GETINCIDENTNUM = "Sp_GetIncidentNum";
        public const string Sp_GETSYSNUM = "Sp_GetSysNum";
        public const string SP_GETUSERLOCKSOURCE = "Sp_GetUserLockSource";
        public const string SP_UPDATEUSERPWDEXP = "Sp_UpdatePasswordExpiry";
        public const string Sp_ArtUserPasswordExpiry = "Sp_ArtUserPasswordExpiry";



        public const string IS_MULTILINGUAL_ENABLED = "Is_Multilingual_Enabled";
        public const string DEFAULT_LANG = "Default_Lang";
        public const string ARTUSERLANG = "ArtUserLang";
        public const string IS_OTP_ENABLED = "Is_OTP_Enabled";
        public const string T24x7SMS = "24x7SMS";
        public const string Snow = "Servicenow";
        public const string Summit = "Summit";
        public const string ITSMCreate = "CREATE";
        public const string ITSMUpdate = "UPDATE";
        public const string Mobile = "mobile";
        public const string Email = "mail";
        public const string SNOWUserMessage = "";
        public const string SNOWGSDMessage = "";
        public const string SNOWCategory = "";
        public const int OTPLength = 6;
        public const int CACHE_DURATION = 1000;
        public const string CHANGE_PASSWORD_SUCCESS = "CHANGE_PASSWORD_SUCCESS";
        public const string CHANGE_PASSWORD_FAILURE = "CHANGE_PASSWORD_FAILURE";
        public const string CHANGE_PASSWORD = "CHANGE_PASSWORD";
        public const string RESET_PASSWORD = "Forgot_Password";
        public const string UNLOCK_ACCOUNT = "Unlock_Account";
        public const string USER_REGISTER = "User_Register";
        public const string INPROGRESS = "InProgress";
        public const string FINISHED = "Finished";
        public const string COMPLETED = "Completed";
        public const string INVALID_ANSWER = "Invalid_Answer";
        public const string INVALID_OTP = "Invalid_OTP";
        public const string ADGIVENNAME = "givenname";
        public const string ADCOUNTRYCODE = "c";
        public const string LDAPNETWORKUSERPASS = "LdapNetworkUserPass";
        public const string NETWORKPASSWORD = "NetWorkPassword";
        public const string PASSPHARSE = "#$hExAwArE$#";
        public const string DOBQUERY = "SELECT D_BIRTH_DATE FROM M_EMPLOYEE_EXCHANGE_VW WHERE S_EMPLOYEE_ID";

        public const string Reset_Password_QA = "ForgotPassword :: Forgot_Password_QA";
        public const string Reset_Password = "ForgotPassword :: Forgot_Password";
        public const string Reset_Password_Login = "ForgotPassword :: Forgot_Password_Login";
        public const string Reset_Password_OTP = "ForgotPassword :: Forgot_Password_OTP";
        public const string Reset_Password_AuthType = "ForgotPassword :: Forgot_Password_AuthType";
        public const string Reset_Password_AuthOTP = "ForgotPassword :: Forgot_Password_AuthOTP";
        public const string Reset_Password_AuthSecQue = "ForgotPassword :: Forgot_Password_AuthSecQue";

        public const string Unlock_Account_Login = "UnlockAccount :: Unlock_Account_Login";
        public const string Unlock_Account = "UnlockAccount :: Unlock_Account";
        public const string Unlock_Account_AuthType = "UnlockAccount :: Unlock_Account_AuthType";
        public const string Unlock_Account_OTP = "UnlockAccount :: Unlock_Account_OTP";
        public const string Unlock_Account_AuthSecQue = "UnlockAccount :: Unlock_Account_AuthSecQue";
        public const string Unlock_Account_AuthOTP = "UnlockAccount :: Unlock_Account_AuthOTP";

        public const string Register_User_Info = "UserEnroll :: Register_User_Info";
        public const string Register_Login = "UserEnroll :: Login";
        public const string Register_Security_Questions = "UserEnroll :: Register";
        public const string Register_MobileNo_Change = "UserEnroll :: Register_MobileNo_Change";



        public const string IncidentLogUpdate = "/IM_LogOrUpdateIncident";
        public const string SummitSymptom = "ART";
        public const string SummitNewStatus = "New";
        public const string SummitAssignedStatus = "Assigned";
        public const string SummitResolvedStatus = "Resolved";
        public const string SummitInProgressStatus = "In-Progress";
        public const string SummitPendingStatus = "Pending";
        public const string SummitImpact = "Individual user";
        //public const string SummitSource = "Nil";
        public const string ResponseTypeJson = "JSON";

        public const int SNOWInProgressStatus = 2;
        public const string SNOWContact_type = "art";
        public const string location = "location";
        public const string display_value = "display_value";
        public const string u_building_name = "u_building_name";
        public const string u_project = "u_project";
        public const string mobile_phone = "mobile_phone";

        public const int SummitPriorityName = 23;
        public const int SLA = 6;
        public const string SummitSLAName = "24/7 Support";
        public const string SummitUrgency = "Non-Critical";
        public const string SummitClassification = "Incident";
        public const string SummitResponse_SLA_Reason = "Account Locked";
        public const string SummitSrvGrp = "Global Service Desk - STG";
        public const string SummitClosureCodeName = "Successful";
        public const string SummitClosureCode = "32";
        public const string SummitSource = "Application";
        public const string SummitLowPriority = "P3 - Low";
        public const string SummitMedium = "Web";
        public const string SummitRestApiNewStatusPageName = "LogTicket";
        public const string SummitRestApiUpdateStatusPageName = "TicketDetail";
        public const string SummitRestApiUpdaterExecutive = "Executive";
        public const string SummitRestApiRequestTypeRemoteCall = "RemoteCall";
        public const string SummitDefaultDateTime = "0001-01-01T00:00:00";
        public const string SummitResponseSLAReason = "Not Applicable";

        //public const string AccounUnlockCategory = "Account Unlock";
        //public const string ChangePasswordCategory = "Password Reset";
        ////public const string ForgotPasswordCategory = "Password Reset";
        //public const string ForgotPasswordCategory = "Forgot Password";
        public const string PASSWORDCHANGEDESCRIPTION = "Your password was reset successfully.";
        public const string ACCOUNTUNLOCKDESCRIPTION = "Your account was unlocked successfully.";
        public const string ARTCATEGORY = "User Account Management";
        
        public const string CHANGEPASSWORDSNOWSHORTDESCRIPTION = "Your request for change password was submitted.";
        public const string ACCOUNTUNLOCKSHORTDESCRIPTION = "Your request for account unlock was submitted.";

        public const string ITSMSNOW = "SNOW";
        public const int NameLength = 10;
        public const string DateTimeformat = "yyyy-MM-ddTHH:mm:ss";

        public const string useragent = "UserAgent";
        public const string EventID = "EventID";
        public const string Device = "Device";
        public const string OS = "OS";
        public const string Browser = "Browser";
        public const string LogFileFolderName = "TrceLogg";

        public const string UserAgent = "UserAgent";
        public const string EventSource = "EventSource";
        public const string Source_Orgin = "0"; // for art web site
        public const string hashkey = "#$hExAwArE$#";
        public const string art_error_head = @"\Images\art_error_head.jpg";
        public const string art_ftr = @"\Images\art_ftr.jpg";

        public const string art_error_headId = "art_error_headId";
        public const string art_ftrId = "art_ftrId";
    }

    public static class RequestMethod
    {
        public const string DELETE = "DELETE";
        public const string GET = "GET";
        public const string POST = "POST";
        public const string PUT = "PUT";
        public const string PATCH = "PATCH";
        public const string ApplicationJson = "application/json";
    }

    public static class SnowState
    {
        public const int New = 1;
        public const int InProgress = 2;
        public const int OnHold = 3;
        public const int Resolved = 6;
        public const int Closed = 7;
        public const int Canceled = 8;
    }

    public static class SnowUrgency
    {
        public const int High = 1;
        public const int Medium = 2;
        public const int Low = 3;
    }

    public static class SnowImpact
    {
        public const int High = 1;
        public const int Medium = 2;
        public const int Low = 3;
    }

    public static class SnowPriority
    {
        public const int Critical = 1;
        public const int High = 2;
        public const int Moderate = 3;
        public const int Low = 4;
        public const int Planning = 5;
    }

    public static class SnowCloseCode
    {
        public const string SolvedWorkAround = "Solved (Work Around)";
        public const string SolvedPermanently = "Solved (Permanently)";
        public const string SolvedRemotelyWorkAround = "Solved Remotely (Work Around)";
        public const string SolvedRemotelyPermanently = "Solved Remotely (Permanently)";
        public const string ClosedResolvedbyCaller = "Closed/Resolved by Caller";
        public const string ResolvedSuccessfully = "Resolved Successfully";
    }
    public static class ConfigKeys
    {
        public const string SENDEREMAIL = "SenderEmail";
        public const string RECEIVEREMAIL = "ReceiverEmail";
        public const string EMAILIP = "EmailIP";
        public const string EMAILPORT = "EmailPort";      
        public const string EMAILUSERNAME = "EmailUserName";
        public const string EMAILPASSWORD = "EmailPassword";
        public const string EMAILDOMAIN = "EmailDomain";
    }

    }
