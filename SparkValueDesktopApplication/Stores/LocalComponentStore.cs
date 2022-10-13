using SparkValueDesktopApplication.Models.Components;
using SparkValueDesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Stores
{
    public class LocalComponentStore
    {
        private readonly IEnumerable<ComponentCategoryViewModel> _componentCategories = new List<ComponentCategoryViewModel>()
        {
             new ComponentCategoryViewModel(
                    "Resistors",
                    new List<ComponentViewModel>()
                    {
                        new ComponentViewModel(
                            new ResistorComponentModel(
                                "Resistor", 
                                "Basic component that adds resistence to the circuit.", 
                                "\\Images\\resistor.png", 
                                5000)),
                        new ComponentViewModel(
                            new ResistorComponentModel(
                                "Potentiometer", 
                                "Variable resistor.", 
                                "\\Images\\potentiometer.png", 
                                1000)),
                    }),
                new ComponentCategoryViewModel(
                    "Diodes",
                    new List<ComponentViewModel>()
                    {
                        new ComponentViewModel(
                            new BasicComponentModel(
                                "Diode",
                                "Basic component that only allows electricity to flow in one direction.",
                                "\\Images\\diode.png")),
                        new ComponentViewModel(
                            new BasicComponentModel(
                                "Light Emmiting Diode",
                                "A diode that emmits lights when energy is passed through it.",
                                "\\Images\\led.png"))
                    }),
                new ComponentCategoryViewModel(
                    "Capacitors",
                    new List<ComponentViewModel>()
                    {
                        new ComponentViewModel(
                            new CapacitorComponentModel(
                                "Capacitor",
                                "Stores electricity that can be discharged even when the circuit is off.",
                                "\\Images\\SettingsGear.png",
                                500))
                    }),
                new ComponentCategoryViewModel(
                    "Transitors",
                    new List<ComponentViewModel>()
                    {
                        new ComponentViewModel(
                            new TransistorComponentModel(
                                "Transistor",
                                "Can be used to make amplification or switching circuits.",
                                "\\Images\\SettingsGear.png"))
                    })
        };

        public IEnumerable<ComponentCategoryViewModel> GetComponentCategories()
        {
            return _componentCategories;
        }
    }
}
