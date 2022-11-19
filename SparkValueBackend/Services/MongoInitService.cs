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
                            "These final two gates build upon what we already know and sound familiar, or they should. NAND, what does that sound like to you? Perhaps AND with an extra N? What does that N represent? Well, a NAND is like an AND but the output goes through an integrated NOT gate. The same thing applies to the NOR gate. These two gates essentially act as two gates in one. Again their logic tables and step-through logic is provided to the left. That is all for logic gates, continue to the next lesson for fun times with integrated circuits."
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
                            "Everything up to this point has been about singular components or gates, but that is about to change. Your world is about to be rocked by integrated circuits. These black boxes with pins are unassuming, but under the hood, they pack a lot of circuitry to condense your circuits into one chip. That one chip can have many different uses, like logic gates, timers, shift registers, sensors, and microcontrollers. Besides uses, integrated circuits come in a variety of package types. These range from usable with very few tools to specialized tools required. The most common through-hole package type is called DIP or Dual In-Line Package. These are great to work with for beginners and fit perfectly on breadboards and other prototyping boards. There are a lot of SMD packages available, however, most are difficult to work with without specialized tools. Due to the variety in pin layouts, I suggest that you read the documentation on your chosen IC to better understand its details. With this knowledge, I challenge you to go and find integrated circuits in your electronics and learn about them. After this lesson on integrated circuits, it is about time we start talking about circuit inputs."
                        },
                        new List<string>
                        {
                            "BlackBoxCircuitry"
                        },
                        new List<int>
                        {
                            4
                        }),
                    new LessonModel("Switches",
                        3,
                        "Pull the lever.", new List<string>
                        {
                            "Pulling levers, actuating sliders, or rocking switches are all great ways to get user input into a circuit. Switches are commonly seen in action when you are turning on the lights or turning on the power to your favorite electronic device or circuit. Switches have two important terms that you will need to know. First, you have poles, which are the number of circuits the switch can control. Then you have throws, these represent the number of states that each pole can connect to. With this knowledge, you now have a basic understanding of switches and how they are categorized. Next, we move to buttons."
                        },
                        new List<string>
                        {
                            "SwitchingSwitches"
                        },
                        new List<int>
                        {
                            1
                        }),
                    new LessonModel("Buttons",
                        4,
                        "Be careful what you press.", new List<string>
                        {
                            "Buttons are an even more common way of getting user input. Think of what is in front of you right now. That keyboard or mouse use buttons to translate human input into what the computer can understand. This concept directly translates to the circuit level too. A reset button here or a keypad of buttons there are basic usages of buttons in circuits. A button has two different natural or resting states, naturally open and closed. The naturally open state is more commonly seen in buttons. It dictates that a button will act as an open circuit until it is actuated. Then you have the opposite state, naturally closed, which dictates that a button will act as a closed circuit until it is actuated. Now that we have covered user input, components, and basic circuit knowledge in the next unit we will finally talk about putting all this knowledge to use and putting it down!"
                        },
                        new List<string>
                        {
                            "ButtonPress"
                        },
                        new List<int>
                        {
                            1
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
                            "As a beginner, using a breadboard is an excellent way to start putting down your components. Even if you are not a beginner breadboards are an essential way to test and prototype your circuits. The most common type of breadboard you will see is called a solderless breadboard. You may also see its less common brother solderable breadboard or perf board. For this lesson, we will be talking about solderless breadboards because of their ease of use. It is also important to note that breadboards are expandable! Just use the notches found on every side to connect another breadboard. Read on to learn more about breadboards and how to place components on them.",
                            "With a breadboard in hand, it may be a little confusing as to where you should place your components. One of the first things you will notice is a gap down the center of your breadboard. This gap allows you to place Dual In-Line Package integrated circuits while keeping the leads separated on different rows. Speaking of rows, they usually consist of five holes to accept component leads. Any components placed in the same row will be connected and share current. However, the different rows are isolated from each other, even across the gap. You may also see a combination of numbering or lettering to help denote hole locations. Then what are these vertical-colored strips? These denote the power rails that run the height of the breadboard. These holes are interconnected vertically, unlike the rows. Know that you know all about breadboards, get out there and practice, tinker with them or use the built-in breadboard that comes with a selection of common components."
                        },
                        new List<string>
                        {
                            "",
                            "ExampleBoard"
                        },
                        new List<int>
                        {
                            0,
                            2
                        }),
                    new LessonModel("Printed Circuit Boards",
                        2,
                        "A green board with all those funky holes.", new List<string>
                        {
                            "Take a step up from a breadboard and you end up at printed circuit boards or PCBs for short. A PCB is a layered circuit, you can get anywhere from two to a hundred or more layers. Using the diagrams on the left, let’s decode what a PCB is. The substrate acts as a separating layer, typically made of a non-conductive material. Then you have thin layers of copper foil that can have holes drilled or traces routed through them. Side note traces act as paths from place to place. On top of the copper comes an iconic green layer called the soldermask. This protects the copper underlayer, covers traces, and identifies solder points to the user. The soldermask can come in almost any color, green is just the most commonly used. On top of the soldermask, a silkscreen can be printed to add lettering, numbers, or other identifiable markings to your PCB. There are other terms that you will see when it comes to PCBs, however, they are not essential to gain a basic idea of what a PCB is. A great way to explore PCBs even further is to take a look into older electronics and explore their PCBs."
                        },
                        new List<string>
                        {
                            "PCBExplodedView"
                        },
                        new List<int>
                        {
                            2
                        }),
                    new LessonModel("Microcontrollers and Beyond",
                        3,
                        "Pre-made circuits with even more possibilities.", new List<string>
                        {
                            "Our circuits, up until this point, only react to our input, but what if we gave them a brain? This is where microcontrollers or microprocessors come into play. Both are similar but different. A microcontroller is an all-in-one computer that can be pre-programmed to execute a single task. In comparison, a microprocessor is only a single part of a computer and needs extra external integrated circuits to function. This is like comparing an Arduino to a Raspberry PI. For those that do not know, an Arduino acts as a development board that can be used alongside your circuits for additional control. Then you have a Raspberry PI, which is a full computer with the same potential as an Arduino with extra bells and whistles, like multitasking. Using a microcontroller unlocks a lot more potential in your circuits. I will not be going into details on programming your microcontroller here, however, there are many great tutorials out there that describe the whole process from start to finish. There are also many kits available that walk you through basic circuits and how they work with an Arduino or other microcontroller. Have fun exploring, the sky is the limit now, you know almost everything when it comes to basic electronics, congrats! In the next unit, we will cover some common tools and items that will aid your journey."
                        },
                        new List<string>
                        {
                            "MicrocontrollersShowAndTell"
                        },
                        new List<int>
                        {
                            4
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
                            "Multimeters are a simple tool useful for troubleshooting your circuits. A basic multimeter can measure voltage, resistance, and current. Another common feature is continuity checking. This is the ability to check if two components are connected electrically. A multimeter is made of three main parts. A display for displaying the reading. A selection knob to switch between different modes of reading values. And at least two ports for connecting probes too. One port should be labeled com for common ground, you should plug in the black probe here. Then plug in the other probe in the other port. If the ports are labeled with symbols insert the probe into the port with the matching symbols for the operation you want. Many types of probes are available and can be used for a variety of use cases, like clips to hold wires or needle-like points to get into tight places.",
                            "To measure voltage or resistance across a component, place one prob on either side to get your reading. If the value is negative, that is okay, just flip the position of the probe. For current, you will need to interrupt the circuit and direct current flow through the meter and then back through your circuit. Use the diagram on the right to get a better understanding of to use a multimeter."
                        },
                        new List<string>
                        {
                            "MultimeterParts",
                            "MultimeterMeasuringValues"
                        },
                        new List<int>
                        {
                            1,
                            2
                        }),
                    new LessonModel("Oscilloscope",
                        2,
                        "Watching the waves of electricity.", new List<string>
                        {
                            "If you find your multimeter lacking the functionality you need to troubleshoot your circuits, then you have come to the right place. An oscilloscope is more advanced and offers even more features to troubleshoot variables over a period. They may look very confusing with all their knobs and sliders and you would be right. To explain all the different functions here would be insane. I highly suggest watching videos on how to use an oscilloscope if you decide you need one. That being said, why would you need one? Their biggest appeal is to have the ability to measure electrical waves or signals and accurately diagnose values like amplitude, frequency, and noise. As a beginner, it is unlikely that you will need one right away unless you work with oscillators or timers extensively."
                        },
                        new List<string>
                        {
                            "OscilloscopeOverview"
                        },
                        new List<int>
                        {
                            4
                        }),
                    new LessonModel("Power Supply",
                        3,
                        "Unlimited POWER!", new List<string>
                        {
                            "Providing power to your circuits is critical to their function. There are several options that you can choose depending on your use case. For many using a USB port is a great option that allows direct connection to a development board or a breadboard power supply. Another common thing is to use wall adapters, most wall adapters end in a barrel plug or act as a USB outlet. Barrel plugs are commonly used for finished circuits and manufactured circuit boards. If you are wanting to get a little more advanced, you can get a dedicated benchtop variable power supply. Getting a benchtop power supply is great if you do a lot of testing with your circuits. In addition, it allows fine control over voltage values and current limits. The last common thing is to use batteries. The usage of batteries is great for mobile projects or other projects that are far from wall outlets. However, you will need to do research into batteries because of the many types, form factors, and capacity.",
                            "For beginners, I recommend that you use either a wall adapter, with a barrel plug or a USB outlet, or a USB cable. This allows you to work with development boards, like the Arduino Uno, and connect to a breadboard power supply easily, with little hassle. If you are looking to upgrade and get seriously into electronics, that is when I would recommend getting a desktop power supply."
                        },
                        new List<string>
                        {
                            "PowerOptions",
                            "BeginnerPowerOption"
                        },
                        new List<int>
                        {
                            1,
                            4
                        }),
                    new LessonModel("Soldering Iron",
                        4,
                        "Fusing metal together, making bonds that last.", new List<string>
                        {
                            "Learning how to solder is a big step that opens many avenues for future projects. There are a lot of things that you may need while you are learning the basics of soldering. Some basic items you may need are flush cutters for trimming leads, a vise or a pair of helping hands to hold your circuits or wires, a heatproof mat to protect your workbench, a solder wick and or a solder vacuum to remove solder, a flux pen if you are using lead-free solder, and most importantly safety glasses. Wait there are different types of solder? Yes, there are two types of solder, their main difference is one contains lead, while the other does not. Often referred to as leaded and lead-free solder. On the next page, we will discuss the anatomy of a soldering iron.",
                            "A soldering iron can be thought of as a hot pen. You have an interchangeable tip that directs the heat into whatever you are working on. A wand that transfers heat to the tip and away from your hand. The control box or base adjusts the temperature of the tip through a digital or analog interface. It is also important to have a stand to rest your soldering iron so it does not roll away from you. Then you need a way to clean your tip, a brass sponge is recommended, but a cool, damp sponge will work too. Now we know the ins and outs of a soldering iron and some accessories, let’s put it into practice.",
                            "The following are some good tips on how to solder. Use the side of your tip to solder, this is considered the sweet spot. Tin your tip with a little bit of solder before heating the piece. Apply your solder to the piece you are heating with the soldering iron. Use a sponge frequently to clean your tip. There are plenty more tips you can find online, and in addition to this lesson’s content, I highly suggest you watch a demonstration first before trying to solder for your first time. Practice makes perfect or at least better joints."
                        },
                        new List<string>
                        {
                            "SolderingExtras",
                            "SolderingAnatomy",
                            "SolderingPractice"
                        },
                        new List<int>
                        {
                            3,
                            2,
                            3
                        }),
                    new LessonModel("Start Making",
                        5,
                        "Show the world your new found skills.", new List<string>
                        {
                            "Now, this is not a tool per se, this is more of a final soap box, a call to action. If you have made it through all these units and lessons, I congratulate you. There has been a ton of information to learn, however, this is only the beginning of your journey. You will learn more as you go on. Through trial and error, you will make projects that test your limits. It is all about practice, learning, and stretching your boundaries. Start simple and build up to more complex projects, all while learning new skills and programs. I have only provided the basics here to guide you along, but now it is time to take off the training wheels and get out there and make cool stuff! I am stepping off my soap box now. I wish you good luck and know that there are many resources you can turn to in times of need or inspiration."
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
