using System;
using System.Linq;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using Morabaraba_2;

namespace Morabaraba.Test
{
    [TestFixture]
    public class Test
    {

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
        public void gameStart()
        {
            IGame g = new Game();
            bool testPass = true;
            foreach (string pos in g.PPositions)
            {
                if (Empty(g.Board, pos))
                {
                    testPass = true;
                }
                else
                {
                    testPass = false; // D1 Failed

                    break;
                }
            }
            Assert.That(testPass == true);
        }

        [Test]
        public void blackPlayerStarts()
        {
            //arrange
            IGame mocked = Substitute.For<IGame>();
            Program.g = mocked; // use the mocked thing
            //act
            Program.Main(new string[0]);
            //assert
            var whatevs_srs_yo = mocked.Received(1).Black;
            mocked.Received().runGame(Arg.Any<Morabaraba_2.Player>());
        }

        [Test]
        public void cowPlacementOnEmptySpace()
        {
            //bool empty = true;
            //foreach(string s in availablePositions)
            //{
            //    if(black.positionsHeld.Contains(s) || white.positionsHeld.Contains(s))
            //    {
            //        empty = false;
            //        break;
            //    }
                
            //}
            //Assert.That(empty == true);
    
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
 
 