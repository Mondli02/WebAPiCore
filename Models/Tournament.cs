using System.Collections.Generic;

namespace webApi.Models
{
    public class Tournament
    {
        public int TournamentId { get; set; }
        public string TournamentName { get; set; } = "CarRacing";

        public User user {get; set; }

        public List<Event> Event {get; set;}

     }
}