using System.Collections.Generic;

namespace Morabaraba_2
{
    public interface IGame
    {
        void printGameBoard(List<string> var);
        void printInstructions();
        void runGame(Player currentPlayer);
        //void ifElse(string ans, Player currentPlayer);
        bool ValidPos(string pos);
        List<string> Board { get; }
        Player Black { get; }
        Player White { get; }
        List<string> PPositions { get; }
        List<string> availablePPosition { get; }
    }
}