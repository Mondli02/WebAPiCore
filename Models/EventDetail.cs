using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Models
{
    public class EventDetail
    {
        public int EventDetailId { get; set; }
        public string EventDetailName { get; set; } = "soccer";
        public int EventDetailNumber { get; set; } = 10;
        
         [Column(TypeName = "decimal(18,4)")]
         public decimal EventDetailOdd { get; set; } = 500;
         public int FinishingPosition { get; set; } = 5;
        public int FirstTimer { get; set; } = 1;

        public int EventDetailStatusId {get; set;}
        public EventDetailStatus EventDetailStatus {get; set;}
        public int EventId {get; set;}
        public Event Event { get; set; }
        

    }
}