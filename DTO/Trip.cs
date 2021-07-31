using System;
using System.Collections.Generic;

namespace TripPinClient.DTO
{
    public class Trip
    {
        public int TripId { get; set; }
        public Guid ShareId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
    }
}
