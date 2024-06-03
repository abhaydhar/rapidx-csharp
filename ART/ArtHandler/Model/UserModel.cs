using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Model
{
    public class UserModel
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

    public class UserEventsModel
    {
        public string EventDate { get; set; }
        public string EventSource { get; set; }
       
    }
    public class eventsData
    {
        public string useragent { get; set; }
        public int EventID { get; set; }
        public string Device { get; set; }
        public string OS { get; set; }
        public string Browser { get; set; }

    }

}
