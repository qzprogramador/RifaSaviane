using System;

namespace SavianeRifa.Models
{
    public class ReservationListItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime RegisteredAt { get; set; }
        public string Location { get; set; } = string.Empty;
        public int TotalRifas { get; set; }
        public int ReservedCount { get; set; }
        public int SoldCount { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
