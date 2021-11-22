using System;

namespace PhoneLogProcessor.Models
{
    /// <summary>
    /// Input.txt fájl adatainak tárolására szolgáló osztály.
    /// </summary>
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
