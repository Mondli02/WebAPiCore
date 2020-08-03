using System;
using System.Collections.Generic;

namespace webApi.Models
{
    public class Event
    {
        public int eventID { get; set; }
        public string eventName { get; set; } ="Semi-final";
        public int eventNumber { get; set; } = 2;
        public DateTime eventDateTime { get; set; }  = DateTime.Now;  
        
        public DateTime eventEndDateTime { get; set; }  = DateTime.Now;  
        public Tournament Tournament {get; set;}

        public EventDetail EventDetail {get; set;}
    }
}