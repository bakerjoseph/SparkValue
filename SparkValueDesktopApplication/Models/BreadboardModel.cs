using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Models
{
    public class BreadboardModel
    {
        public List<ComponentModel> Components { get; }
        
        public BreadboardModel()
        {
            Components = new List<ComponentModel>();
        }

        /// <summary>
        /// Add a new component to the breadboard's list of components.
        /// </summary>
        /// <param name="component">A new component</param>
        public void AddComponent(ComponentModel component)
        {
            Components.Add(component);
        }

        /// <summary>
        /// Add a range of components to the breadboard's list of components.
        /// </summary>
        /// <param name="components">A IEnumerable of components</param>
        public void AddComponents(IEnumerable<ComponentModel> components)
        {
            Components.AddRange(components);
        }

        /// <summary>
        /// Remove a component at a given index in the breadboard's list of components.
        /// </summary>
        /// <param name="index">A integer index of a component</param>
        public void RemoveComponentAt(int index)
        {
            if (index < 0 || index > Components.Count) return;
            Components.RemoveAt(index);
        }
    }
}
