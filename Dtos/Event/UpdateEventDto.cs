using System;
using webApi.Models;
namespace webApi.Dtos.Event
{
    public class UpdateEventDto
    {
        public int eventID { get; set; }
        public string eventName { get; set; } ="Semi-final";
        public int eventNumber { get; set; } = 2;
        public DateTime eventDateTime { get; set; }  = DateTime.Now;  
        
        public DateTime eventEndDateTime { get; set; }  = DateTime.Now;  
         public webApi.Models.Tournament Tournament {get; set;}
    }
}