using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Models
{
    public class ComponentModel
    {
        public string Name { get; }
        public string Description { get; }
        public double InputVoltage { get; }
        public double OutputVoltage { get; }
        public double InputCurrent { get; }
        public double OutputCurrent { get; }
        public double ResistenceValue { get; }

        public ComponentModel(string name, string description)
        {
            Name = name;
            Description = description;
        }

        // TODO: computation of values! Plus probably more.
    }
}
