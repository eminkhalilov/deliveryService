using System;
using System.Collections.Generic;
using System.Text;

namespace Farfetch.DeliveryService.Models
{
    public interface IFarfetchConfiguration
    {
        string ConnectionString { get; set; }
    }
}
