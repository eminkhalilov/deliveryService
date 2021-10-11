using Farfetch.DeliveryService.Models.Points;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farfetch.DeliveryService.Models.Routes
{
    public class CalculatedRoute
    {
        public string RouteSchema { get; set; }
        public decimal TotalTime { get; set; }
        public decimal TotalCost { get; set; }
        public Point[] PointRoutes { get; set; }
    }
}
