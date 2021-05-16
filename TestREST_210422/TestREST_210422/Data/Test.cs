using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace TestREST_210422.Data
{
    public class Test
    {
        [JsonProperty("eventId")]
        public int EventId { get; set; }

        [JsonProperty("eventTitle")]
        public string EventTitle { get; set; }

        [JsonProperty("eventDescription")]
        public string EventDescription { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }
        
        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty("avenue")]
        public string Avenue { get; set; }

        [JsonProperty("maxMembers")]
        public int MaxMembers { get; set; }
    }
}
