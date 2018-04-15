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
    }
}
