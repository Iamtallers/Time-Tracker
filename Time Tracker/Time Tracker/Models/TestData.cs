using System;
using System.Collections.Generic;
using System.Text;
using Time_Tracker.Services;

namespace Time_Tracker.Models
{
    public class TestData : IIdentifiable 
    {
       public string Id { get; set; }
        public int Age { get; set; }
        public double Amount { get; set; }
        public bool Flag { get; set; }
        public string Name { get; set; }
    }
}
