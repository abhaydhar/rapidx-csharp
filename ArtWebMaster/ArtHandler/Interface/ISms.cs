using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Interface
{
    public interface ISms
    {
        bool SendSMS(string userId, string mobileNo, string activity, string userName,string otp, string userCountryCode = null);
        string SendEnrollmentSMS(string userId, string mobileNo, string activity, string userName, string activityType,string sendOTP,string userCountryCode = null);
    }
}
