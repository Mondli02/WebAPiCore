using System;
using System.Collections.Generic;

namespace webApi.Dtos.Event
{
    public class GetEventDto
    {
        public int eventID { get; set; }
        public string eventName { get; set; } 
        public int eventNumber { get; set; } = 2;
        public DateTime eventDateTime { get; set; } 
        
        public DateTime eventEndDateTime { get; set; } 
        public webApi.Models.Tournament Tournament {get; set;}

        

    }
}