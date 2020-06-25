using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Location : Entity
    {
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
