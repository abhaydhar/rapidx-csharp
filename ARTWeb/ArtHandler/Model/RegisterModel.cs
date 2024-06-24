using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Model
{
    public class RegisterModel
    {
        public int TotalNumberOfQuestions { get; set; }
        public int TotalQuestionsToAnswer { get; set; }
        public List<QuestionsCategoryModel> questions { get; set; }
        public bool IsLogin { get; set; }
    }

    public class ResetModel
    {
        public int TotalQuestionsToAnswer { get; set; }
        public bool IsOtpEnabled { get; set; }
    }

    public class AccountUnlockModel
    {
        public int TotalQuestionsToAnswer { get; set; }
    }
    public class UserInfoModel
    {
        public string MobileNum { get; set; }
        public string DOB { get; set; }
        public string CountryCode { get; set; }
        public string IsOTPSent { get; set; }
        public string IsValidOTP { get; set; }
        public string OTPValidateMsg { get; set; }
        public List<CountryCodeModel> lstContryCodes { get; set; }
        public bool IsMobileNumPrivate { get; set; }
    }
}
