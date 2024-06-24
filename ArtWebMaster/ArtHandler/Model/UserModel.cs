using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Model
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool UserAccountStatus { get; set; }
        public string UserDOB { get; set; }
        public string MobileNumber { get; set; }
        public string IsRegistered { get; set; }
        public bool IsOTPEnabled { get; set; }
        public string PasswordAge { get; set; }
        public string PasswordExpired { get; set; }
        public string OUName { get; set; }
        public bool IsEnrollmentLink { get; set; }
        public bool IsInValid { get; set; }
    }
    public class AccountLockModelResponse
    {
        public bool islocked { get; set; }
        public string waitTime { get; set; }
    }
    public class AccountLockModel
    {
        public string userid { get; set; }
        public int attemptcount { get; set; }
        public DateTime attemptdatetime { get; set; }
    }
    public class UserOtpAttemptModel
    {
        public int islocked { get; set; }
        public string attemptcount { get; set; }
        public string maxattempt { get; set; }
    }
    public class UserInfoModel
    {
        public int id { get; set; }
        public string userid { get; set; }
        public bool isotpenabled { get; set; }
        public DateTime otpenableddatetime { get; set; }
        public bool isregistered { get; set; }
        public DateTime registereddatetime { get; set; }
        public string mobilenumber { get; set; }
        public string countrycode { get; set; }
        public bool ismobilenumberprivate { get; set; }      
        public string IsOTPSent { get; set; }
        public string IsValidOTP { get; set; }
        public string OTPValidateMsg { get; set; }
        public List<CountryCodeModel> lstContryCodes { get; set; }
        public string SentOTP { get; set; }
    }
    public class RptUserModel
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
        public string Defaultlandingurl { get; set; }
        public bool Isadmin { get; set; }
        public bool IsReadOnly { get; set; }
    }
    public class UserAccessPrivilege
    {
        public string entityname { get; set; }      
    }
}
