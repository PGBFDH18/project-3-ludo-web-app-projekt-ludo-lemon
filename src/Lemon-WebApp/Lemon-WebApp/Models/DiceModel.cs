using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lemon_WebApp.Models
{
    public class DiceDot
    {
        public string Id { get; set; }
    }

    public class DiceModel
    {
        private Dictionary<int, List<DiceDot>> DiceDictionary { get; set; }
        public List<DiceDot> currentDicePosition { get; set; }
        public int DiceValue { get; set; }

        public DiceModel(int diceValue)
        {
            DiceValue = diceValue;
            DiceDictionary = new Dictionary<int, List<DiceDot>>
            {
                {
                    1, new List<DiceDot>
                    {
                        new DiceDot
                        {
                             Id ="center"
                        }
                    }
                },
                {
                    2, new List<DiceDot>
                    {

                        new DiceDot
                        {
                             Id ="leftUpper"
                        },
                         new DiceDot
                        {
                             Id ="rightLower"
                        }
                    }
                },
                {
                    3, new List<DiceDot>
                    {

                        new DiceDot
                        {
                             Id ="leftUpper"
                        },
                        new DiceDot
                        {
                             Id ="center"
                        },
                        new DiceDot
                        {
                             Id ="rightLower"
                        }
                    }
                },
                {
                    4, new List<DiceDot>
                    {

                        new DiceDot
                        {
                             Id ="leftUpper"
                        },
                        new DiceDot
                        {
                             Id ="leftLower"
                        },
                        new DiceDot
                        {
                             Id ="rightUpper"
                        },
                        new DiceDot
                        {
                             Id ="rightLower"
                        }
                    }
                },
                {
                    5, new List<DiceDot>
                    {

                        new DiceDot
                        {
                             Id ="leftUpper"
                        },
                        new DiceDot
                        {
                             Id ="leftLower"
                        },
                        new DiceDot
                        {
                             Id ="center"
                        },
                        new DiceDot
                        {
                             Id ="rightUpper"
                        },
                        new DiceDot
                        {
                             Id ="rightLower"
                        }
                    }
                },
                {
                    6, new List<DiceDot>
                    {

                        new DiceDot
                        {
                             Id ="leftUpper"
                        },
                        new DiceDot
                        {
                             Id ="leftMiddel"
                        },
                        new DiceDot
                        {
                             Id ="leftLower"
                        },
                        new DiceDot
                        {
                             Id ="rightUpper"
                        },
                        new DiceDot
                        {
                             Id ="rightMiddel"
                        },
                        new DiceDot
                        {
                             Id ="rightLower"
                        }
                    }
                }
            };
            currentDicePosition = DiceDictionary[DiceValue];
        }
    }
}
