using MongoDB.Bson;
using MongoDB.Driver;
using SparkValueBackend.Models;
using SparkValueBackend.Services.UnitCreators;
using SparkValueBackend.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoUtils = SparkValueBackend.Services.Utils.MongoUtils;

namespace SparkValueBackend.Services
{
    public class MongoInitService : MongoUtils
    {
        private IUnitCreator _unitCreator;

        private readonly string _unitCollection;

        List<UnitModel> _defaultUnits = new List<UnitModel>()
        {
            new UnitModel("Shocking Introduction",
                1,
                "Basic concepts to explore.",
                new List<LessonModel>
                {
                    new LessonModel("Electricity Primer",
                        1,
                        "An intro to electricity.", new List<string>
                        {
                            "We all ask what electricity is because to most it is this powerful, invisible force that keeps our lives moving. However, it is not all as mysterious as some declare it to be. This power comes from atoms and the flow of electrons between them. I will not be getting all sciency here about the reasons why they move. Just keep in mind that electrons move in a path from a negative to a positive source. But what do electrons move through? How do electrons follow a certain path? The answer to both questions is simply yes. Electrons move through everything and anything, it is just a matter of how conductive the material or thing is. The more conductive the material is, the easier it is for electrons to flow through it. The less conductive the material is, the harder it is for electrons to flow through it. These are called conductors and insulators, respectively. Think of a wire, the copper core is a conductor that lets electrons flow easily and the hard plastic or rubber casing prevents those electrons from escaping the conductor.",
                            "With that basic knowledge of electrons, conductors, and insulators, let us move into more exciting terms to understand our circuit better. Some of the most common things you will hear are Voltage, Current, and Resistance. Voltage is the difference in charge between two points. Current is the rate at which the charge flows. And Resistance is a material’s tendency to resist or impede the flow of charge. How do these new terms relate? Well, it all boils down to a fundamental law that describes these relationships and their effect in a circuit. This is Ohm’s Law. The mathematical formula is as follows: Voltage divided by the product of the Current and Resistance. Voltage is represented by a V or E in older diagrams and is measured in Volts. Current is represented by an I and measured in Amps. And Resistance is represented by an R and measured in Ohms. See the below diagram and examples for a more visual interpretation of Ohm’s Law.",
                            "One last thing, I earlier said that electrons flow from a negative to a positive source. But isn’t that wrong? Actually no. Many people learn that electricity flows from a positive to a negative source. I hope you are sitting down for this, but this is wrong or at least partially wrong. What we have been taught is known as Conventional Flow, which is the working, standard practice to use in most places. However, Electron Flow is the real deal. Although Electron Flow is more accurate, this application will show everything in Conventional Flow for ease of use. There are more examples of Ohm’s Law on the right and just as a teaser, next lesson we will dive into circuits!"
                        },
                        new List<string>
                        {
                            "",
                            "OhmsLawCollective",
                            "OhmsLawAdvanced"
                        },
                        new List<int>
                        {
                            0,
                            3,
                            1
                        }),
                    new LessonModel("Reading Circuit Diagrams",
                        2,
                        "Learn how to read circuit diagrams so you can build your own.", new List<string>
                        {
                            "Circuit diagrams can be complex, so let’s demystify them. First things first, let’s define some vocabulary. First up, a circuit can have one or more load devices. A load device is an object, like a light bulb or motor, that uses the current flowing through the circuit. Speaking of flowing through a circuit, one of the most important things is wires. On diagrams, if a wire ends in a circle or node that means it is connected to another wire or other components. Nodes are considered junctions between items. If a wire goes under another wire without a node present that means they are not connected. These paths are also important. A node can connect to a singular item or multiple. When it connects to a singular item that item is in series and if it connects to multiple items those items are in parallel. Series and parallel circuits are shown in more detail above. Also, be careful when reconstructing circuits from diagrams. Make sure there is always a path from your positive terminal to the negative terminal and there is some kind of load in that path. Next up we will be talking about some of the archaic, alien symbols found in circuit diagrams.",
                            "Now I will not be discussing every symbol and every variation of these symbols, but I will show off the popular symbols that you will see most commonly. In addition, if you are looking for more details on these components keep reading, you will learn more about them in due time. As you can see to the left there is a vast amount of these symbols. The most common symbols are resistors, diodes, transistors, and capacitors. There are also symbols for your power supply or battery and switches. Some other symbols pictured here include inductors, logic gates, and microcontrollers. This barely scratches the surface of what you will see, but this gives you a general idea of what to expect on circuit diagrams. Next time, we will start diving deeper into the world of components."
                        },
                        new List<string>
                        {
                            "LoadsAndNodes",
                            "ExampleDiagrams"
                        },
                        new List<int>
                        {
                            4,
                            2
                        }),
                    new LessonModel("Discussion of Components",
                        3,
                        "A brief introduction to the vast world of components.", new List<string>
                        {
                            "Electronics have evolved over the years, at first, all our components were large and bulky. Now they are as small as grains of rice or even smaller. However, for beginner electronics, we will not dwell in the realm of Surface Mounted Devices, or SMD for short. SMD components are smaller and more efficient in the manufacturing process. However, they are harder to learn from and use as a beginner. This is why we will be focusing on THT or Through Hole Technology. THT components are considerably harder to work with in manufacturing since holes need to be drilled through circuit boards, but are excellent for beginners and rapid prototyping. Through the next two units, you will learn more about specific components and their usage. If you are still curious about our evolution in circuit design, crack open older electronics and compare them to current devices. Happy exploring! Now, it is time to go down the rabbit hole of components."
                        },
                        new List<string>
                        {
                            ""
                        },
                        new List<int>
                        {
                            0
                        })
                }),
            new UnitModel("Components Galore",
                2,
                "Basic components and what they do.",
                new List<LessonModel>
                {
                    new LessonModel("Resistors",
                        1,
                        "Resist that power, bring it down.", new List<string>
                        {
                            "Resistors are a fundamental component that is widely used for many different purposes. Its main goal is to limit the flow of electrons flowing through a circuit. Resistors are measured in Ohms, seen as the greek capital letter omega. Through-hole resistor values are derived from the colored bands that encircle them. You will see four to six colored bands, each with a separate meaning. Below you will find a more in-depth color chart and calculator that you can play around with and get a better understanding of resistors."
                        },
                        new List<string>
                        {
                            "ResistorsGalore"
                        },
                        new List<int>
                        {
                            3
                        }),
                    new LessonModel("Diodes",
                        2,
                        "Manage the flow of electricity.", new List<string>
                        {
                            "Diodes have a couple of important usages that are common in many circuits. At the bare minimum, a diode controls the direction of current flow in a circuit based on its polarity. The polarity of diodes only allows current to flow from the positive anode to the negative cathode. To tell the pins apart on a through-hole diode, a colored band is present on the cathode end of the component. One common thing that uses diodes is power supplies, acting as a rectifier and reverse current protection. There are other specialized diodes that I will not discuss here but we will discuss the all-important light-emitting diodes in the next lesson."
                        },
                        new List<string>
                        {
                            "CurrentCheckPoints"
                        },
                        new List<int>
                        {
                            2
                        }),
                    new LessonModel("Light Emitting Diodes",
                        3,
                        "An indication of a lively circuit.", new List<string>
                        {
                            "This niche type of diode is an excellent addition to any circuit. As the name implies this diode emits light when current is passed through it. They also have the same polarity rules as a diode, this time though it is about the length of the lead on the component. The longer lead or leg is the anode and the shorter one is the cathode. More current passing through an LED means that the light will be brighter, but be warned that too much power will damage the LED. You will also see many subtypes of LEDs, like RGB addressable LEDs, UV LEDs, Infrared LEDs, and so much more. Now go out there and light up your projects!"
                        },
                        new List<string>
                        {
                            "LightsOfLEDs"
                        },
                        new List<int>
                        {
                            2
                        }),
                    new LessonModel("Capacitors",
                        4,
                        "Storing electricity like a champ.", new List<string>
                        {
                            "Another common component is the capacitor. This bad boy is like a rechargeable battery. His main goal is to store energy locally in your circuit. Capacitors have a capacitance that is a measure of charge capacity. This value is measured in farads, F, and is commonly used with metric prefixes since a single farad is a lot of capacitance. This is why you see capacitors with pico, nano, micro, and milli-farads commonly. These are used in a variety of circuits to act as signal filters, decoupling/bypass capacitors, and many other applications. Continue reading to learn how to choose capacitors for your projects and some common types of them.",
                            "There are a lot of factors that affect your choice of capacitors. First, the size matters, both the volume and the capacitance. Second, the max voltage rating is extremely important, you do not want your capacitors to explode or pop. Third, the leakage current, the tiny amount of current that leaks through your capacitor. Fourth, the equivalent series resistance, a value of internal resistance that dissipates energy. Lastly, tolerance, a capacitor is noted by a nominal value, but can be plus or minus one percent to twenty percent. Knowing those factors let's look at common capacitor types. The most common type is ceramic-based capacitors. They are small, both in size and capacitance, but offer low ESR and leakage current. The next common type of capacitor is an electrolytic capacitor. They are more commonly used in high-voltage circuits due to their high maximum voltage rating and high capacitance. Although, they suffer higher average current leakage making them unsuitable for energy storage. There are even capacitors called supercapacitors that have super high capacitance but low maximum voltage ratings. And so many more but those are just some of the types of capacitors you will see and hear about."
                        },
                        new List<string>
                        {
                            "ElectricityStore",
                            "CapacitorTypes"
                        },
                        new List<int>
                        {
                            1,
                            3
                        }),
                    new LessonModel("Transistors",
                        5,
                        "An electrical checkpoint.", new List<string>
                        {
                            "Transistors are versatile components that are used in almost every circuit. There are two main types of them, bi-polar junction, BJT, and metal-oxide field-effect, MOSFET. In this lesson, we will be covering BJT and more specifically the NPN version. The visual guide below will walk you through what an NPN transistor is and what it can be used for. Beyond that transistors are used to make integrated circuits and act as signal amplification. I have not included a lot of details here due to the complex nature of transistors. However, I encourage you to tinker with them and learn more about them."
                        },
                        new List<string>
                        {
                            "NPNInAction"
                        },
                        new List<int>
                        {
                            3
                        }),
                    new LessonModel("Inductors",
                        6,
                        "Magnetic fields of stored electricity.", new List<string>
                        {
                            "Inductors are a less common component but they are used in many signal filtering and power circuits. Inductors are complicated for what they are. Even though the most simple inductor is a loop of coiled wire, the inductance value can be hard to compute. This value is measured in Henry and is generally associated with a prefix like milli, micro, or nano. I will not be discussing how to do the math for inductance because there are multiple variables, like how many coils, what is the core made of, and several others items. Does this inductance matter? Why yes, it does. In signal filtering a low inductance value filters and smooths out high-frequency noise. On the opposite end, a high inductance value filters and smooths out low-frequency noise. There are many more components to discuss through the next unit, so have fun and keep learning!"
                        },
                        new List<string>
                        {
                            "InductorMagic"
                        },
                        new List<int>
                        {
                            4
                        })
                }),
            new UnitModel("Miscellaneous Components",
                3,
                "Even more components and what they do.",
                new List<LessonModel>
                {
                    new LessonModel("Logic Gates",
                        1,
                        "Logic at its finest.", new List<string>
                        {
                            "Now that we are moving into the digital realm of electronics, our signals can perform logical operations using logic gates. There are six total gates with distinct operations. Each gate takes in at least two inputs and outputs the result based on that operation. Digital true and false are represented here by a voltage value. The voltage value can vary, but commonly 5 or 3 V are used to represent a true value, and 0 V is a false value. Another common thing you will see is truth tables. They specify what the output of a gate should be based on the inputs. The first pair of gates we will discuss are ANDs and ORs.",
                            "AND and OR gates are the most basic logic gates. Let’s first look at an AND gate. It will only output true if both inputs are true. Then you have an OR gate. It outputs true if at least one of its inputs is true. You can follow this logic on the left and view the respective truth tables for reference. The next pair of gates we will discuss are NOTs and XORs.",
                            "NOT gates might not be considered a gate because it does not perform a logical operation. Its job is to flip the input, this means it will turn a true input into a false output. Then you have an XOR gate. This logic gate is like an OR gate, but with one major difference. That being, only one input can be true to get an output of true. And same as before the logic and respective truth tables are on the left. The next pair of gates we will discuss are NANDs and NORs. Sound familiar?",
                            "These final two gates build upon what we already know and sound familiar, or they should. NAND, what does that sound like to you? Perhaps AND with an extra N? What does that N represent? Well, a NAND is like an AND but the output goes through an integrated NOT gate. The same thing applies to the NOR gate. These two gates essentially act as two gates in one. Again their logic tables and step through logic is provided to the left. That is all for logic gates, continue on to the next lesson for fun times with integrated circuits."
                        },
                        new List<string>
                        {
                            "GateSymbols",
                            "ANDsORs",
                            "NOTsXORs",
                            "NANDsNORs"
                        },
                        new List<int>
                        {
                            4,
                            2,
                            2,
                            2
                        }),
                    new LessonModel("Integrated Circuits",
                        2,
                        "Tiny circuits within circuits.", new List<string>
                        {
                            ""
                        },
                        new List<string>
                        {
                            ""
                        },
                        new List<int>
                        {
                            0
                        }),
                    new LessonModel("Switches",
                        3,
                        "Pull the lever.", new List<string>
                        {
                            ""
                        },
                        new List<string>
                        {
                            ""
                        },
                        new List<int>
                        {
                            0
                        }),
                    new LessonModel("Buttons",
                        4,
                        "Be careful what you press.", new List<string>
                        {
                            ""
                        },
                        new List<string>
                        {
                            ""
                        },
                        new List<int>
                        {
                            0
                        })
                }),
            new UnitModel("Putting it Down",
                4,
                "A neat place for every little thing.",
                new List<LessonModel>
                {
                    new LessonModel("Breadboards",
                        1,
                        "A board for bread and wires.", new List<string>
                        {
                            ""
                        },
                        new List<string>
                        {
                            ""
                        },
                        new List<int>
                        {
                            0
                        }),
                    new LessonModel("Printed Circuit Boards",
                        2,
                        "A green board with all those funky holes.", new List<string>
                        {
                            ""
                        },
                        new List<string>
                        {
                            ""
                        },
                        new List<int>
                        {
                            0
                        }),
                    new LessonModel("Microcontrollers and Beyond",
                        3,
                        "Pre-made circuits with even more possibilities.", new List<string>
                        {
                            ""
                        },
                        new List<string>
                        {
                            ""
                        },
                        new List<int>
                        {
                            0
                        })
                }),
            new UnitModel("Common Tools",
                5,
                "Tools of the trade.",
                new List<LessonModel>
                {
                    new LessonModel("Multimeter",
                        1,
                        "A way to monitor your ins and outs.", new List<string>
                        {
                            ""
                        },
                        new List<string>
                        {
                            ""
                        },
                        new List<int>
                        {
                            0
                        }),
                    new LessonModel("Oscilloscope",
                        2,
                        "Watching the waves of electricity.", new List<string>
                        {
                            ""
                        },
                        new List<string>
                        {
                            ""
                        },
                        new List<int>
                        {
                            0
                        }),
                    new LessonModel("Power Supply",
                        3,
                        "Unlimited POWER!", new List<string>
                        {
                            ""
                        },
                        new List<string>
                        {
                            ""
                        },
                        new List<int>
                        {
                            0
                        }),
                    new LessonModel("Soldering Iron",
                        4,
                        "Fusing metal together, making bonds that last.", new List<string>
                        {
                            ""
                        },
                        new List<string>
                        {
                            ""
                        },
                        new List<int>
                        {
                            0
                        }),
                    new LessonModel("Start Making",
                        5,
                        "Show the world your new found skills.", new List<string>
                        {
                            ""
                        },
                        new List<string>
                        {
                            ""
                        },
                        new List<int>
                        {
                            0
                        })
                })
        };

        public MongoInitService(string connectionString, string databaseName, string unitCollection) : base(connectionString, databaseName)
        {
            _unitCollection = unitCollection;
            _unitCreator = new MongoUnitCreator(connectionString, databaseName, _unitCollection);

            InitializeUnits();
        }

        private async void InitializeUnits()
        {
            IMongoCollection<UnitModel> unitCollection = ConnectToMongo<UnitModel>(_unitCollection);
            if (unitCollection.CountDocuments(new BsonDocument()) > 0) return;

            // If there are no documents in the units collection add the default units
            foreach (UnitModel unit in _defaultUnits)
            {
                await _unitCreator.CreateUnit(unit);
            }
        }
    }
}
