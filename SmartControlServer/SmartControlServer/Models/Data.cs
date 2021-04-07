using System.Collections.Generic;

namespace SmartControlServer.Models
{
    public static class Data
    {
        public static List<Sensor> Sensors { get; set; } = new List<Sensor>();
        public static List<Rule> Rules { get; set; } = new List<Rule>();
    }
}