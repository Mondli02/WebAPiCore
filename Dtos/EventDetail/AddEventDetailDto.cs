namespace webApi.Dtos.EventDetail
{
    public class AddEventDetailDto
    {
        public int eventID { get; set; }
         public int EventDetailStatusId {get; set;}
        public string eventDetailName { get; set; }
        public int eventDetailNumber { get; set; }
        public decimal eventDetailOdd { get; set; }
        public int finishingPosition { get; set; }
         public int firstTimer { get; set; }
        

    }
}