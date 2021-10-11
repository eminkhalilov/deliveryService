using System;
using System.Collections.Generic;
using System.Text;

namespace Farfetch.DeliveryService.Models.Routes
{
    public class NameRoute : Route
    {
        public string SourcePointName { get; set; }
        public string DestinationPointName { get; set; }
    }
}
