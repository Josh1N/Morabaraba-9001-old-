using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morabaraba_2
{
    public class Player
    {
        public int onBoard { get; set; }

        public List<string> positionsHeld { get; set; }

        public string state { get; set; }

        public int unPlaced { get; set; }

        public string name { get; set; }

        public char place { get; set; }

        public List<string[]> playerMills { get; set; }
    }


}
