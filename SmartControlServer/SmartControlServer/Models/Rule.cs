using System.Collections.Generic;

namespace SmartControlServer.Models
{
    public class Rule
    {
        public string Id { get; set; }
        public List<(string id, bool value)> Sensors { get; set; } = new List<(string id, bool value)>();

        public Rule(string id)
        {
            Id = id;
        }
    }
}