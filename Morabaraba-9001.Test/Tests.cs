using System;
using System.Linq;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace Morabaraba.Test
{
    [TestFixture]
    public class Test
    {
        public static List<string> Positions = new List<string> { "A1", "A4", "A7", "B2", "B4", "B6", "C3", "C4", "C5", "D1", "D2", "D3", "D5", "D6", "D7", "E3", "E4", "E5", "F2", "F4", "F6", "G1", "G4", "G7" };
        public static List<string> availablePositions = new List<string> { "A1", "A4", "A7", "B2", "B4", "B6", "C3", "C4", "C5", "D1", "D2", "D3", "D5", "D6", "D7", "E3", "E4", "E5", "F2", "F4", "F6", "G1", "G4", "G7" };

        public static Player black = new Player();
        public static Player white = new Player();


        public static bool ValidPos(string pos)
        {
            if (availablePositions.Contains(pos))
            {
                return true;
            }
            return false;
        }


        public static Player SwitchPlayer(Player x)
        {
            //first check if position is valid then



            if (x == black)
            {
                return white;
            }
            else
                return black;



        }

        static void ifElse(string ans, Player currentPlayer)
        {
            if (ValidPos(ans))
            {
                Placing(board, ans, currentPlayer);

                availablePositions.Remove(ans);
                currentPlayer.positionsHeld.Add(ans);

                if (currentPlayer.state == "Placing")
                {
                    currentPlayer.unPlaced = currentPlayer.unPlaced - 1;
                    currentPlayer.onBoard = currentPlayer.onBoard + 1;
                }

                printGameBoard(board);

                checkPlayerState(currentPlayer);
                checkMills(currentPlayer);

                currentPlayer = SwitchPlayer(currentPlayer);
                runGame(currentPlayer);
            }
            else
            {
                Console.WriteLine("The position is not valid. Please enter a valid position to place a cow on the board.");
                ans = Console.ReadLine().ToUpper();
                ifElse(ans, currentPlayer);
            }
        }

        static void checkPlayerState(Player currentPlayer)
        {
            if (currentPlayer.unPlaced > 0)
            {
                currentPlayer.state = "Placing";
            }
            else if (currentPlayer.unPlaced == 0)
            {
                currentPlayer.state = "Moving";
            }
            else if (currentPlayer.onBoard < 4 && currentPlayer.unPlaced == 0)
            {
                currentPlayer.state = "Flying";
            }
            else if (black.state == "Flying" && black.onBoard < 3)
            {
                // game over - White wins 
                Console.Clear();
                Console.WriteLine("White Player wins!!! Well done :)");
            }
            else if (white.state == "Flying" && white.onBoard < 3)
            {
                // game over - Black wins 
                Console.Clear();
                Console.WriteLine("Black Player wins!!! Well done :)");
            }
        }

        // have a named array for each possible mill
        static string[] m0 = new string[] { "A1", "A4", "A7" };
        static string[] m1 = new string[] { "B2", "B4", "B6" };
        static string[] m2 = new string[] { "C3", "C4", "C5" };
        static string[] m3 = new string[] { "D1", "D2", "D3" };
        static string[] m4 = new string[] { "D5", "D6", "D7" };
        static string[] m5 = new string[] { "E3", "E4", "E5" };
        static string[] m6 = new string[] { "F2", "F4", "F6" };
        static string[] m7 = new string[] { "G1", "G4", "G7" };
        static string[] m8 = new string[] { "A1", "D1", "G1" };
        static string[] m9 = new string[] { "B2", "D2", "F2" };
        static string[] m10 = new string[] { "C3", "D3", "E3" };
        static string[] m11 = new string[] { "A4", "B4", "C4" };
        static string[] m12 = new string[] { "E4", "F4", "G4" };
        static string[] m13 = new string[] { "C5", "D5", "E5" };
        static string[] m14 = new string[] { "B6", "D6", "F6" };
        static string[] m15 = new string[] { "A7", "D7", "G7" };
        static string[] m16 = new string[] { "A1", "B2", "C3" };
        static string[] m17 = new string[] { "A7", "B6", "C5" };
        static string[] m18 = new string[] { "G1", "F2", "E3" };
        static string[] m19 = new string[] { "G7", "F6", "E5" };

        // put mill arrays into a list
        static List<string[]> availableMills = new List<string[]> { m0, m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15, m16, m17, m18, m19 };

        static void checkMills(Player currentPlayer)
        {
            // go through player's positionsHeld list to see if they have any of the mills
            // if they do, remove mill from list and add to player's personal mill list 
            bool gotMills = false;

            // puts new mills into a players list and removes them from availableMills
            foreach (string[] mill in availableMills)
            {
                if (currentPlayer.positionsHeld.Contains(mill[0]) && currentPlayer.positionsHeld.Contains(mill[1]) && currentPlayer.positionsHeld.Contains(mill[2]))
                {
                    gotMills = true;
                    currentPlayer.playerMills.Add(mill);
                }
            }
            foreach (string[] mill in currentPlayer.playerMills)
            {
                availableMills.Remove(mill);
            }

            // removes broken mills from players list and puts them back in availableMills

            // this mechanism prevents mills from being used twice beacause the mill, once made and used, will remain on the player's list,
            // and therefor not available, until broken and reformed. 
            foreach (string[] mill in currentPlayer.playerMills)
            {
                if (mill != null)
                {
                    if (!(currentPlayer.positionsHeld.Contains(mill[0]) && currentPlayer.positionsHeld.Contains(mill[1]) && currentPlayer.positionsHeld.Contains(mill[2])))
                    {
                        availableMills.Add(mill);
                    }
                }

            }
            foreach (string[] mill in availableMills)
            {
                currentPlayer.playerMills.Remove(mill);
            }

            // player must then be presented with option to kill
            // must design killing mechanism 
            if (gotMills == true)
            {
                millKill(currentPlayer);
            }
        }

        public static void Killing(List<string> board, string pos, Player currentPlayer)
        {

            string rowA = board[1];
            char posA1 = rowA[2];
            char posA4 = rowA[12];
            char posA7 = rowA[22];

            string rowB = board[3];
            char posB2 = rowB[5];
            char posB4 = rowB[12];
            char posB6 = rowB[19];

            string rowC = board[5];
            char posC3 = rowC[8];
            char posC4 = rowC[12];
            char posC5 = rowC[16];

            string rowD = board[6];
            char posD1 = rowD[2];
            char posD2 = rowD[5];
            char posD3 = rowD[8];
            char posD5 = rowD[16];
            char posD6 = rowD[19];
            char posD7 = rowD[22];

            string rowE = board[7];
            char posE3 = rowE[8];
            char posE4 = rowE[12];
            char posE5 = rowE[16];

            string rowF = board[8];
            char posF2 = rowF[5];
            char posF4 = rowF[12];
            char posF6 = rowF[19];

            string rowG = board[9];
            char posG1 = rowG[2];
            char posG4 = rowG[12];
            char posG7 = rowG[22];


            switch (pos)
            {
                //A
                case "A1":
                    posA1 = 'O';
                    string updateLine = board[1].Remove(2, 1);
                    updateLine = updateLine.Insert(2, posA1.ToString());
                    board[1] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "A4":
                    posA4 = 'O';
                    updateLine = board[1].Remove(12, 1);
                    updateLine = updateLine.Insert(12, posA4.ToString());
                    board[1] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "A7":
                    posA7 = 'O';
                    updateLine = board[1].Remove(22, 1);
                    updateLine = updateLine.Insert(22, posA7.ToString());
                    board[1] = updateLine;
                    Positions.Remove(pos);
                    break;

                //B
                case "B2":
                    posB2 = 'O';
                    updateLine = board[3].Remove(5, 1);
                    updateLine = updateLine.Insert(5, posB2.ToString());
                    board[3] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "B4":
                    posB4 = 'O';
                    updateLine = board[3].Remove(12, 1);
                    updateLine = updateLine.Insert(12, posB2.ToString());
                    board[3] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "B6":
                    posB4 = 'O';
                    updateLine = board[3].Remove(19, 1);
                    updateLine = updateLine.Insert(19, posB2.ToString());
                    board[3] = updateLine;
                    Positions.Remove(pos);
                    break;
                case "C3":
                    posC3 = 'O';
                    updateLine = board[5].Remove(8, 1);
                    updateLine = updateLine.Insert(8, posC3.ToString());
                    board[5] = updateLine;
                    Positions.Remove(pos);
                    break;
                case "C4":
                    posC4 = 'O';
                    updateLine = board[5].Remove(12, 1);
                    updateLine = updateLine.Insert(12, posC4.ToString());
                    board[5] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "C5":
                    posC5 = 'O';
                    updateLine = board[5].Remove(16, 1);
                    updateLine = updateLine.Insert(16, posC5.ToString());
                    board[5] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "D1":
                    posD1 = 'O';
                    updateLine = board[7].Remove(2, 1);
                    updateLine = updateLine.Insert(2, posD1.ToString());
                    board[7] = updateLine;
                    Positions.Remove(pos);
                    break;


                case "D2":
                    posD2 = 'O';
                    updateLine = board[7].Remove(5, 1);
                    updateLine = updateLine.Insert(5, posD2.ToString());
                    board[7] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "D3":
                    posD3 = 'O';
                    updateLine = board[7].Remove(8, 1);
                    updateLine = updateLine.Insert(8, posD3.ToString());
                    board[7] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "D5":
                    posD5 = 'O';
                    updateLine = board[7].Remove(16, 1);
                    updateLine = updateLine.Insert(16, posD5.ToString());
                    board[7] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "D6":
                    posD6 = 'O';
                    updateLine = board[7].Remove(19, 1);
                    updateLine = updateLine.Insert(19, posD6.ToString());
                    board[7] = updateLine;
                    Positions.Remove(pos);
                    break;
                case "D7":
                    posD7 = 'O';
                    updateLine = board[7].Remove(22, 1);
                    updateLine = updateLine.Insert(22, posD7.ToString());
                    board[7] = updateLine;
                    Positions.Remove(pos);
                    break;

                //E
                case "E3":
                    posE3 = 'O';
                    updateLine = board[9].Remove(8, 1);
                    updateLine = updateLine.Insert(8, posE3.ToString());
                    board[9] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "E4":
                    posE4 = 'O';
                    updateLine = board[9].Remove(12, 1);
                    updateLine = updateLine.Insert(12, posE4.ToString());
                    board[9] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "E5":
                    posE5 = 'O';
                    updateLine = board[9].Remove(16, 1);
                    updateLine = updateLine.Insert(16, posE3.ToString());
                    board[9] = updateLine;
                    Positions.Remove(pos);
                    break;

                //F
                case "F2":
                    posF2 = 'O';
                    updateLine = board[11].Remove(5, 1);
                    updateLine = updateLine.Insert(5, posF2.ToString());
                    board[11] = updateLine;
                    Positions.Remove(pos);
                    break;


                case "F4":
                    posF4 = 'O';
                    updateLine = board[11].Remove(12, 1);
                    updateLine = updateLine.Insert(12, posF4.ToString());
                    board[11] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "F6":
                    posF6 = 'O';
                    updateLine = board[11].Remove(19, 1);
                    updateLine = updateLine.Insert(19, posF6.ToString());
                    board[11] = updateLine;
                    Positions.Remove(pos);
                    break;
                //G
                case "G1":
                    posG1 = 'O';
                    updateLine = board[13].Remove(2, 1);
                    updateLine = updateLine.Insert(2, posG1.ToString());
                    board[13] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "G4":
                    posG4 = 'O';
                    updateLine = board[13].Remove(12, 1);
                    updateLine = updateLine.Insert(12, posG4.ToString());
                    board[13] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "G7":
                    posG7 = 'O';
                    updateLine = board[13].Remove(22, 1);
                    updateLine = updateLine.Insert(22, posG7.ToString());
                    board[13] = updateLine;
                    Positions.Remove(pos);
                    break;
            }
        }

        static string[] check(string ans, Player player)
        {
            foreach (string[] mill in player.playerMills)
            {
                List<string> check = new List<string>();
                check = mill.ToList();
                if (check.Contains(ans))
                {
                    return mill;
                }
            }
            return null;
        }

        static void millKill(Player currentPlayer)
        {
            // after player has killed with a mill, that mill should be added to another list which indicates that it may not be used again immediately 

            // must design that mechanism 

            // must also when checking for mills remember to remove broken mills from a player's list and put them back on the available mills list

            Console.WriteLine("You've made a mill! choose one of the other player's cows to kill:");
            string ans = Console.ReadLine().ToUpper();
            if (currentPlayer == black)
            {
                if (white.positionsHeld.Contains(ans))
                {
                    white.playerMills.Add(check(ans, white));

                    Killing(board, ans, currentPlayer);

                    availablePositions.Add(ans);
                    white.positionsHeld.Remove(ans);
                    white.onBoard = white.onBoard - 1;

                    printGameBoard(board);

                    checkPlayerState(currentPlayer);

                    currentPlayer = SwitchPlayer(currentPlayer);
                    runGame(currentPlayer);
                }
                else
                {
                    Console.WriteLine("The position is not valid. Please enter a valid position to place a cow on the board.");
                    ans = Console.ReadLine().ToUpper();
                    millKill(currentPlayer);
                }
            }
            else
            {
                if (black.positionsHeld.Contains(ans))
                {
                    black.playerMills.Add(check(ans, black));

                    Killing(board, ans, currentPlayer);

                    availablePositions.Add(ans);
                    black.positionsHeld.Remove(ans);
                    black.onBoard = white.onBoard - 1;

                    printGameBoard(board);

                    checkPlayerState(currentPlayer);

                    currentPlayer = SwitchPlayer(currentPlayer);
                    runGame(currentPlayer);
                }
                else
                {
                    Console.WriteLine("The position is not valid. Please enter a valid position to place a cow on the board.");
                    ans = Console.ReadLine().ToUpper();
                    millKill(currentPlayer);
                }
            }
        }

        static bool chackAdjacency(string ans, string ans1)
        {
            bool adjacent = false;

            foreach (string[] mill in availableMills)
            {
                if (ans == mill[0] && ans1 == mill[1])
                {
                    adjacent = true;
                }
                else if (ans == mill[1] && ans1 == mill[2])
                {
                    adjacent = true;
                }
            }

            if (adjacent == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void runGame(Player currentPlayer)
        {
            checkPlayerState(currentPlayer);
            checkMills(currentPlayer);

            if (currentPlayer.state == "Placing")
            {
                Console.WriteLine(string.Format("Unplaced Cows: {0} Cows on Board: {1}", currentPlayer.unPlaced.ToString(), currentPlayer.onBoard.ToString()));
                Console.WriteLine(string.Format("State: {0}", currentPlayer.state));
                Console.WriteLine(string.Format("Player {0} enter a position to place cow.", currentPlayer.name));

                string ans = Console.ReadLine().ToUpper();

                ifElse(ans, currentPlayer);

                Console.WriteLine(Positions);
                Console.ReadLine();
            }
            else if (currentPlayer.state == "Moving")
            {
                /* onve the state changes from Placing to Moving, the input message needs to change to 'move __ to __'
                 * so it will require three input validations:
                 * -> is the first input a position currently help by the current player?
                 * -> is the second position available?
                 * -> and is it adjacent to the first position?
                 */
                Console.WriteLine(string.Format("Unplaced Cows: {0} Cows on Board: {1}", currentPlayer.unPlaced.ToString(), currentPlayer.onBoard.ToString()));
                Console.WriteLine(string.Format("State: {0}", currentPlayer.state));
                Console.WriteLine(string.Format("Player {0} enter the position of the cow you would like to move.", currentPlayer.name));

                string ans = Console.ReadLine().ToUpper();

                if (currentPlayer.positionsHeld.Contains(ans)) // does not check adjacency yet
                {
                    Killing(board, ans, currentPlayer);
                    Console.WriteLine(string.Format("Player {0} enter the position you would like to move your cow to.", currentPlayer.name));
                    string ans1 = Console.ReadLine().ToUpper();
                    // this is where the adjacency test needs to happen
                    if (chackAdjacency(ans, ans1))
                    {
                        ifElse(ans1, currentPlayer);
                    }
                    else
                    {
                        Console.WriteLine("The position is not valid. Please enter a valid position to place a cow on the board.");
                        runGame(currentPlayer);
                    }
                }
                else
                {
                    Console.WriteLine("The position is not valid. Please enter a valid position to place a cow on the board.");
                    runGame(currentPlayer);
                }
            }
            else if (currentPlayer.state == "Flying")
            {
                // when the state changes from Moving to Flying, the third validation can just be ignored. 
                Console.WriteLine(string.Format("Unplaced Cows: {0} Cows on Board: {1}", currentPlayer.unPlaced.ToString(), currentPlayer.onBoard.ToString()));
                Console.WriteLine(string.Format("State: {0}", currentPlayer.state));
                Console.WriteLine(string.Format("Player {0} enter the position of the cow you would like to move.", currentPlayer.name));

                string ans = Console.ReadLine().ToUpper();

                if (currentPlayer.positionsHeld.Contains(ans))
                {
                    Console.WriteLine(string.Format("Player {0} enter the position you would like to move your cow to.", currentPlayer.name));
                    string ans1 = Console.ReadLine().ToUpper();
                    ifElse(ans1, currentPlayer);
                }
                else
                {
                    Console.WriteLine("The position is not valid. Please enter a valid position to place a cow on the board.");
                    runGame(currentPlayer);
                }
            }

            /* so first we need to start out by asking Player1 (lets use 1 and 2 instead of B and W) for their
             * first move -> so we're going to need some input validation here. 
             * 
             * if the move is valid, the board must be updated and runGame called again (with the opposite
             * player to the one who just went as currentPlayer) 
             * 
             * the state of currentPlayer must be checked with every move
             */
        }


        static void printInstructions()
        {
            Console.Write("Morabaraba Game Instructions\n\nHOW TO START\n1. 	To start you need the Morabaraba game board. \n	The board starts empty, each player holding all his pieces (or 'cows') \n	in hand.\n2. 	Each player has 12 cows. One player plays white, \n	the other black.\n\nRULES\n1. 	The gameboard for Morabaraba.\n2. 	When a player is reduced to 3 pieces, his pieces are free \n	to move to any unoccupied point (or 'fly'), instead of being restricted \n	to adjacent points as earlier in the game.\n\nGAMEPLAY\n1. 	At first, each player in turn puts one piece on the board, \n	at any vacant point.\n2. 	Once all pieces are on the board, a player now moves one of \n	his pieces along a marked line to an adjacent empty point.\n3. 	If a piece placed or moved forms a row of three along a marked\n 	line (This is called a mill), he can take one of his opponent's \n	pieces (ie. kill an oponents 'cow'), as long as that piece is not itself part of a mill.\n4. 	If when capturing, all opposing pieces have formed mills, then\n 	any of the pieces may be captured.\n\nHOW TO WIN\n1. 	The goal of the game is to reduce your opponent's pieces to as\n 	little as possible.\n2. 	A player wins the game when his opponent is reduced to 2 pieces \n	and is thus unable to form a mill or make any further captures.\n3. 	If the board is filled in the first phase, and no pieces taken, \n	the second phase will be gridlocked with neither player able to move.\n 	In this case the game is draw.\n\n");
        }


        public static List<string> board = new List<string>()
        {
            "  1  2  3   4   5  6  7  \n",
            "A O ------- O ------- O  \n",  //A1 =[1][2]    A4=[1][12]      A7=[1][22]
            "  | '       |       ' |  \n",
            "B |  O ---- O ---- O  |  \n",  //B2 =[3][5]    B4=[3][12]      B6=[3][19]
            "  |  | '    |   '  |  |  \n",
            "C |  |  O - O - O  |  |  \n",  //C3 =[5][8]    C4 =[5][12]     C5=[5][16]
            "  |  |  |       |  |  |  \n",
            "D O -O- O       O -O- O  \n",  //D1=[7][2]     D2=[7][5]       D3=[7][8]     D5=[7][16]     D6=[7][19]    D7=[7][22]
            "  |  |  |       |  |  |  \n",
            "E |  |  O - O - O  |  |  \n",  //E3=[9][8]     E4=[9][12]      E5=[9][16]
            "  |  | '    |    ' |  |  \n",
            "F |  O ---- O ---- O  |  \n",  //F2=[11][5]    F4=[11][12]     F6=[11][19]    
            "  | '       |       ' |  \n",
            "G O ------- O ------- O  \n\n"  //G1=[13][2]   G4=[13][12]     G7=[13][22]
        };


        public static void printGameBoard(List<string> var)
        {
            // prints list of strings for each row of the board
            // strings in the list can be updated via their index whenever the board changes 
            // this is going to be similar to what we tried to do in F# but it'll be alot easier because
            // its just a matter of indexing and updating at that index 
            foreach (string r in var)
            {
                Console.WriteLine(r);
            }

        }


        public static void Placing(List<string> board, string pos, Player currentPlayer)
        {

            string rowA = board[1];
            char posA1 = rowA[2];
            char posA4 = rowA[12];
            char posA7 = rowA[22];

            string rowB = board[3];
            char posB2 = rowB[5];
            char posB4 = rowB[12];
            char posB6 = rowB[19];

            string rowC = board[5];
            char posC3 = rowC[8];
            char posC4 = rowC[12];
            char posC5 = rowC[16];

            string rowD = board[6];
            char posD1 = rowD[2];
            char posD2 = rowD[5];
            char posD3 = rowD[8];
            char posD5 = rowD[16];
            char posD6 = rowD[19];
            char posD7 = rowD[22];

            string rowE = board[7];
            char posE3 = rowE[8];
            char posE4 = rowE[12];
            char posE5 = rowE[16];

            string rowF = board[8];
            char posF2 = rowF[5];
            char posF4 = rowF[12];
            char posF6 = rowF[19];

            string rowG = board[9];
            char posG1 = rowG[2];
            char posG4 = rowG[12];
            char posG7 = rowG[22];


            switch (pos)
            {
                //A
                case "A1":
                    posA1 = currentPlayer.place;
                    string updateLine = board[1].Remove(2, 1);
                    updateLine = updateLine.Insert(2, posA1.ToString());
                    board[1] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "A4":
                    posA4 = currentPlayer.place;
                    updateLine = board[1].Remove(12, 1);
                    updateLine = updateLine.Insert(12, posA4.ToString());
                    board[1] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "A7":
                    posA7 = currentPlayer.place;
                    updateLine = board[1].Remove(22, 1);
                    updateLine = updateLine.Insert(22, posA7.ToString());
                    board[1] = updateLine;
                    Positions.Remove(pos);
                    break;

                //B
                case "B2":
                    posB2 = currentPlayer.place;
                    updateLine = board[3].Remove(5, 1);
                    updateLine = updateLine.Insert(5, posB2.ToString());
                    board[3] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "B4":
                    posB4 = currentPlayer.place;
                    updateLine = board[3].Remove(12, 1);
                    updateLine = updateLine.Insert(12, posB4.ToString());
                    board[3] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "B6":
                    posB6 = currentPlayer.place;
                    updateLine = board[3].Remove(19, 1);
                    updateLine = updateLine.Insert(19, posB6.ToString());
                    board[3] = updateLine;
                    Positions.Remove(pos);
                    break;
                case "C3":
                    posC3 = currentPlayer.place;
                    updateLine = board[5].Remove(8, 1);
                    updateLine = updateLine.Insert(8, posC3.ToString());
                    board[5] = updateLine;
                    Positions.Remove(pos);
                    break;
                case "C4":
                    posC4 = currentPlayer.place;
                    updateLine = board[5].Remove(12, 1);
                    updateLine = updateLine.Insert(12, posC4.ToString());
                    board[5] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "C5":
                    posC5 = currentPlayer.place;
                    updateLine = board[5].Remove(16, 1);
                    updateLine = updateLine.Insert(16, posC5.ToString());
                    board[5] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "D1":
                    posD1 = currentPlayer.place;
                    updateLine = board[7].Remove(2, 1);
                    updateLine = updateLine.Insert(2, posD1.ToString());
                    board[7] = updateLine;
                    Positions.Remove(pos);
                    break;


                case "D2":
                    posD2 = currentPlayer.place;
                    updateLine = board[7].Remove(5, 1);
                    updateLine = updateLine.Insert(5, posD2.ToString());
                    board[7] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "D3":
                    posD3 = currentPlayer.place;
                    updateLine = board[7].Remove(8, 1);
                    updateLine = updateLine.Insert(8, posD3.ToString());
                    board[7] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "D5":
                    posD5 = currentPlayer.place;
                    updateLine = board[7].Remove(16, 1);
                    updateLine = updateLine.Insert(16, posD5.ToString());
                    board[7] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "D6":
                    posD6 = currentPlayer.place;
                    updateLine = board[7].Remove(19, 1);
                    updateLine = updateLine.Insert(19, posD6.ToString());
                    board[7] = updateLine;
                    Positions.Remove(pos);
                    break;
                case "D7":
                    posD7 = currentPlayer.place;
                    updateLine = board[7].Remove(22, 1);
                    updateLine = updateLine.Insert(22, posD7.ToString());
                    board[7] = updateLine;
                    Positions.Remove(pos);
                    break;

                //E
                case "E3":
                    posE3 = currentPlayer.place;
                    updateLine = board[9].Remove(8, 1);
                    updateLine = updateLine.Insert(8, posE3.ToString());
                    board[9] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "E4":
                    posE4 = currentPlayer.place;
                    updateLine = board[9].Remove(12, 1);
                    updateLine = updateLine.Insert(12, posE4.ToString());
                    board[9] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "E5":
                    posE5 = currentPlayer.place;
                    updateLine = board[9].Remove(16, 1);
                    updateLine = updateLine.Insert(16, posE5.ToString());
                    board[9] = updateLine;
                    Positions.Remove(pos);
                    break;

                //F
                case "F2":
                    posF2 = currentPlayer.place;
                    updateLine = board[11].Remove(5, 1);
                    updateLine = updateLine.Insert(5, posF2.ToString());
                    board[11] = updateLine;
                    Positions.Remove(pos);
                    break;


                case "F4":
                    posF4 = currentPlayer.place;
                    updateLine = board[11].Remove(12, 1);
                    updateLine = updateLine.Insert(12, posF4.ToString());
                    board[11] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "F6":
                    posF6 = currentPlayer.place;
                    updateLine = board[11].Remove(19, 1);
                    updateLine = updateLine.Insert(19, posF6.ToString());
                    board[11] = updateLine;
                    Positions.Remove(pos);
                    break;
                //G
                case "G1":
                    posG1 = currentPlayer.place;
                    updateLine = board[13].Remove(2, 1);
                    updateLine = updateLine.Insert(2, posG1.ToString());
                    board[13] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "G4":
                    posG4 = currentPlayer.place;
                    updateLine = board[13].Remove(12, 1);
                    updateLine = updateLine.Insert(12, posG4.ToString());
                    board[13] = updateLine;
                    Positions.Remove(pos);
                    break;

                case "G7":
                    posG7 = currentPlayer.place;
                    updateLine = board[13].Remove(22, 1);
                    updateLine = updateLine.Insert(22, posG7.ToString());
                    board[13] = updateLine;
                    Positions.Remove(pos);
                    break;
            }
        }

        public static void createPlayers()
        {
            black.name = "Black";
            white.name = "White";

            black.place = 'B';
            white.place = 'W';

            black.unPlaced = 12;
            white.unPlaced = 12;

            black.onBoard = 0;
            white.onBoard = 0;

            black.state = "Placing";
            white.state = "Placing";

            black.playerMills = new List<string[]>();
            white.playerMills = new List<string[]>();

            black.positionsHeld = new List<string>();
            white.positionsHeld = new List<string>();
        }
        public static bool Empty(List<string> board, string pos)
        {

            string rowA = board[1];
            char posA1 = rowA[2];
            char posA4 = rowA[12];
            char posA7 = rowA[22];

            string rowB = board[3];
            char posB2 = rowB[5];
            char posB4 = rowB[12];
            char posB6 = rowB[19];

            string rowC = board[5];
            char posC3 = rowC[8];
            char posC4 = rowC[12];
            char posC5 = rowC[16];

            string rowD = board[6];
            char posD1 = rowD[2];
            char posD2 = rowD[5];
            char posD3 = rowD[8];
            char posD5 = rowD[16];
            char posD6 = rowD[19];
            char posD7 = rowD[22];

            string rowE = board[7];
            char posE3 = rowE[8];
            char posE4 = rowE[12];
            char posE5 = rowE[16];

            string rowF = board[8];
            char posF2 = rowF[5];
            char posF4 = rowF[12];
            char posF6 = rowF[19];

            string rowG = board[9];
            char posG1 = rowG[2];
            char posG4 = rowG[12];
            char posG7 = rowG[22];


            switch (pos)
            {
                //A
                case "A1":
                    if (posA1 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "A4":
                    if (posA4 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "A7":
                    if (posA7 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                //B
                case "B2":
                    if (posB2 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "B4":
                    if (posB4 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "B6":
                    if (posB6 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "C3":
                    if (posC3 == 'O')
                    {
                        return true;
                    }
                    else { return false; }
                case "C4":
                    if (posC4 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "C5":
                    if (posC5 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "D1":
                    if (posD1 == 'O')
                    {
                        return true;
                    }
                    else { return false; }


                case "D2":
                    if (posD2 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "D3":
                    if (posD3 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "D5":
                    if (posD5 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "D6":
                    if (posD6 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "D7":
                    if (posD7 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                //E
                case "E3":
                    if (posE3 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "E4":
                    if (posE4 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "E5":
                    if (posE5 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                //F
                case "F2":
                    if (posF2 == 'O')
                    {
                        return true;
                    }
                    else { return false; }


                case "F4":
                    if (posF4 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "F6":
                    if (posF6 == 'O')
                    {
                        return true;
                    }
                    else { return false; }
                //G
                case "G1":
                    if (posG1 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "G4":
                    if (posG4 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                case "G7":
                    if (posG7 == 'O')
                    {
                        return true;
                    }
                    else { return false; }

                default:
                    return false;
            }
        }

        //Placement testing 
        /*
         * When the game starts, the board is empty
         * The player with dark cows is given the first chance to move
         * Cows can only be placed on empty spaces
         * A maximum of 12 placements per player are allowed
         * Cows cannot be moved during placement
        */


        [Test]
        public void gameStart(List<string> Positions)
        {
            bool testPass = true;
            foreach (string pos in Positions)
            {
                if (Empty(board, pos))
                {
                    testPass = true;
                }
                else
                {
                    testPass = false;
                    break;
                }
            }
            Assert.That(testPass == true);
        }

        [Test]
        public void blackPlayerStarts()
        {
            

        }

        [Test]
        public void cowPlacementOnEmptySpace()
        {

        }

        [Test]
        public void twelveCowsPerPlayer(Player player)
        {
            Assert.That(player.onBoard <= 12);
        }

        [Test]
        public void cantMovePlacedCows()
        {

        }

        /*
         * During moving,
         * ■ A cow can only move to another connected space
         * ■ A cow can only move to an empty space
         * ■ Moving does not increase or decrease the number of cows on theboard
         * 
         */
        [Test]
        public void canOnlyMoveToAdjacentPlace()
        {

        }

        [Test]
        public void canOnlyMoveToEmptySpace()
        {

        }

        [Test]
        public void numberOfCowsOnBoardDoesntChange()
        {

        }

        //During flying,
        //■ Cows can move to any empty space if only three cows of that colorremain
        [Test]
        public void canFly()
        {

        }

        /*In general,
         * ■ A mill is formed by three cows of the same color in a line
         * ■ A mill is not formed when
         *      ● Cows in a line are of different colors
         *      ● Connected spaces containing cows do not form a line
         * ■ Shooting is only possible on the turn that a mill is completed, despitethe mill persisting for another turn
         * ■ A cow in a mill, when non-mill cows exist, cannot be shot
         * ■ A cow in a mill, when all cows are in mills, can be shot
         * ■ A player cannot shoot their own cows
           ■ A player cannot shoot an empty space
           ■ Shot cows are removed from the board
           ■ A win occurs if an opponent cannot move
           ■ A win occurs if an opponent has two or fewer cows, and placement isfinished 
           */

        [Test]
        public void millsFormed()
        {

        }

        [Test]
        public void millNotFormed()
        {

        }

        [Test]
        public void onlyKillWhenMillFormed()
        {

        }

        [Test]
        public void cannotKillMillCows()
        {

        }

        [Test]
        public void canKillMIllCows()
        {

        }

        [Test]
        public void cantKillOwnCow()
        {

        }

        [Test]
        public void cannotShootEmptySpace()
        {

        }

        [Test]
        public void deadCowsRemoved()
        {

        }

        [Test]
        public void winCantMove()
        {

        }

        [Test]
        public void winLessThanTwoCows()
        {

        }
    }
}
 
 