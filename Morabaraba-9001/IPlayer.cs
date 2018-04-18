using System.Collections.Generic;

namespace Morabaraba_2
{
    public interface IPlayer
    {
        string name { get; set; }
        int onBoard { get; set; }
        char place { get; set; }
        List<string[]> playerMills { get; set; }
        List<string> positionsHeld { get; set; }
        string state { get; set; }
        int unPlaced { get; set; }
    }
}