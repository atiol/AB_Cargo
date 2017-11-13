using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AB_CargoServices.Models
{
    public class CargoSummary
    {
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public long TrackingNumber { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
        public double Price { get; set; }
        public DateTime Sent { get; set; }
        public string Description { get; set; }
    }
}