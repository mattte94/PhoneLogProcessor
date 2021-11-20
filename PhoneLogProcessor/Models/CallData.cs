using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneLogProcessor.Models
{
    public class CallData
    {
        public DateTime CallDate { get; set; }
        public DateTime CallTime { get; set; }
        public string CallerPersonPhoneNumber { get; set; }
        public string CalledPersonPhoneNumber { get; set; }
        public int CallDuration { get; set; }

        public CallData(DateTime callDate, DateTime callTime, string callerPersonPhoneNumber, string calledPersonPhoneNumber, int callDuration)
        {
            CallDate = callDate;
            CallTime = callTime;
            CallerPersonPhoneNumber = callerPersonPhoneNumber;
            CalledPersonPhoneNumber = calledPersonPhoneNumber;
            CallDuration = callDuration;
        }
    }
}
