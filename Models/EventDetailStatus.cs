using System.Collections.Generic;

namespace webApi.Models
{
    public class EventDetailStatus
    {
        public int EventDetailStatusId { get; set; }
        public string EventDetailStatusName { get; set; } = "Active";

        public List<EventDetail> EventDetail { get; set; }

    }
}