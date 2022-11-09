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
                "Basic concepts to explore.",
                new List<LessonModel>
                {
                    new LessonModel("Electricity Primer",
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
                "Basic components and what they do.",
                new List<LessonModel>
                {
                    new LessonModel("Resistors",
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
                "Even more components and what they do.",
                new List<LessonModel>
                {
                    new LessonModel("Logic Gates",
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
                "A neat place for every little thing.",
                new List<LessonModel>
                {
                    new LessonModel("Breadboards",
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
                "Tools of the trade.",
                new List<LessonModel>
                {
                    new LessonModel("Multimeter",
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

        private void InitializeUnits()
        {
            IMongoCollection<UnitModel> unitCollection = ConnectToMongo<UnitModel>(_unitCollection);
            if (unitCollection.CountDocuments(new BsonDocument()) > 0) return;

            // If there are no documents in the units collection add the default units
            foreach (UnitModel unit in _defaultUnits)
            {
                _unitCreator.CreateUnit(unit);
            }
        }
    }
}
