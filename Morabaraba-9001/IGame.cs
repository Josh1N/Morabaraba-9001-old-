using System.Collections.Generic;

namespace Morabaraba_2
{
    public interface IGame
    {
        void printGameBoard(List<string> var);
        void printInstructions();
        void runGame(Player currentPlayer);
        List<string> Board { get; }
        Player Black { get; }
        List<string> PPositions { get; }
    }
}