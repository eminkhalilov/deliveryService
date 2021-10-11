using System;
using System.Collections.Generic;
using System.Text;

namespace Farfetch.DeliveryService.Models.Routes
{
    public class Route
    {
        public long Id { get; set; }
        public long SourcePointId { get; set; }
        public long DestinationPointId { get; set; }
        public decimal Time { get; set; }
        public decimal Cost { get; set; }
    }
}
