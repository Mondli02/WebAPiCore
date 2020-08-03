using System;

namespace webApi.Dtos.Event
{
    public class AddEventDto
    {
        public string EventName { get; set; }
        public int Eventnumber { get; set; }     
        public DateTime  EventDate { get; set; }
        public DateTime EventDateTime { get; set; }
          public int TournamentId { get; set; }
    }
}