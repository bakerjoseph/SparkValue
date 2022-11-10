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
                    new LessonModel("Reading Circuit Diagrams",
                        2,
                        "Learn how to read circuit diagrams so you can build your own.", new List<string>
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
                    new LessonModel("Discussion of Components",
                        3,
                        "A brief introduction to the vast world of components.", new List<string>
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
            new UnitModel("Components Galore",
                2,
                "Basic components and what they do.",
                new List<LessonModel>
                {
                    new LessonModel("Resistors",
                        1,
                        "Resist that power, bring it down.", new List<string>
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
                    new LessonModel("Diodes",
                        2,
                        "Manage the flow of electricity.", new List<string>
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
                    new LessonModel("Light Emitting Diodes",
                        3,
                        "An indication of a lively circuit.", new List<string>
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
                    new LessonModel("Capacitors",
                        4,
                        "Storing electricity like a champ.", new List<string>
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
                    new LessonModel("Transistors",
                        5,
                        "An electrical checkpoint.", new List<string>
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
                    new LessonModel("Inductors",
                        6,
                        "Magnetic fields of stored electricity.", new List<string>
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
            new UnitModel("Miscellaneous Components",
                3,
                "Even more components and what they do.",
                new List<LessonModel>
                {
                    new LessonModel("Logic Gates",
                        1,
                        "Logic at its finest.", new List<string>
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
