using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Model
{
    public class UserEventsModelDateWise
    {
        public string Date { get; set; }
        public string Attempts { get; set; }
    }
    public class UserEventsModel
    {
        public string EventDate { get; set; }
        public string EventSource { get; set; }
        public string UserId { get; set; }
        public string Eventdt { get; set; }
    }
    public class UserAgentModel
    {
        public string EventDate { get; set; }
        public string Browsername { get; set; }
        public string OSname { get; set; }
        public string UserId { get; set; }
        public string UserAgent { get; set; }
        public string EventID { get; set; }
        public string[] substrings { get; set; }
        public string[] device { get; set; }
        public string deviceinfo { get; set; }
        public string ClientIP { get; set; }
    }
    public class UserAgentInput
    {
        public string UserId { get; set; }
        public string EventDate { get; set; }
        public string EventSource { get; set; }
        public string Eventinput { get; set; }


    }
    public class UserEventsInput
    {
        public string UserId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Mode { get; set; }
    }
    public class FrequentAccountlockoutUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Count { get; set; }
    }

    public class DeviceLockout
    {
        public string EventSource { get; set; }
        public string Count { get; set; }
    }

    public class eventsData
    {
        public string UserID { get; set; }
        public DateTime EventTimestamp { get; set; }
        public string EventSource { get; set; }
        public string HostMachine { get; set; }
        public string EventDesc { get; set; }
        public string UserAgent { get; set; }
        public int EventId { get; set; }
        public string ClientIP { get; set; }
        public string ActivityID { get; set; }
        public string OSName { get; set; }

    }

}
