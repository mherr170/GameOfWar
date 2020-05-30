using System;
using System.Linq;
using System.Collections.Generic;
using GameOfWar.DTO;
using GameOfWar.Utility;

namespace GameOfWar.PrintLogic.PrintStatic
{
    static class PrintStatic
    {

        #region Print Functions with Parameters

        public static void PrintRemainingHumanCards(Player humanPlayer)
        {
            Console.WriteLine("");
            Console.WriteLine("You have " + humanPlayer.PlayerCards.Count + " cards left in your hand.");
            Console.WriteLine("");
        }

        public static void PrintWarFinalCard(Card humanWarCard, Card computerWarCard)
        {
            PrintWarFlavorText();

            Console.WriteLine("");
            Console.WriteLine("Your final war card is the " + StaticGameUtility.TranslateNumberToFaceCard(humanWarCard.CardValue) + " of " + humanWarCard.CardSuit + "!");
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("The computer's war card is the " + StaticGameUtility.TranslateNumberToFaceCard(computerWarCard.CardValue) + " of " + computerWarCard.CardSuit + "!");
            Console.WriteLine("");
        }

        public static void PrintRoundWinHuman(Player computerPlayer)
        {
            Console.WriteLine("");
            Console.WriteLine("You won this round!");
            Console.WriteLine("You have placed your card and the " + StaticGameUtility.TranslateNumberToFaceCard(computerPlayer.PlayerCards.First().CardValue) + " of " + computerPlayer.PlayerCards.First().CardSuit + " into the bottom of your hand.");
            Console.WriteLine("");
        }

        public static void PrintRoundWinComputer(Player humanPlayer)
        {
            Console.WriteLine("");
            Console.WriteLine("You lost this round!");
            Console.WriteLine("The computer has taken its card and the " + StaticGameUtility.TranslateNumberToFaceCard(humanPlayer.PlayerCards.First().CardValue) + " of " + humanPlayer.PlayerCards.First().CardSuit + " and placed it into the bottom of its hand.");
            Console.WriteLine("");
        }

        public static void PrintCurrentMove(Player humanPlayer, Player computerPlayer)
        {
            Console.WriteLine("");
            Console.WriteLine("You play the " + StaticGameUtility.TranslateNumberToFaceCard(humanPlayer.PlayerCards.First().CardValue) + " of " + humanPlayer.PlayerCards.First().CardSuit + "!");
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("The computer plays the " + StaticGameUtility.TranslateNumberToFaceCard(computerPlayer.PlayerCards.First().CardValue) + " of " + computerPlayer.PlayerCards.First().CardSuit + "!");
            Console.WriteLine("");
        }

        public static void PrintHumanWinsWar(List<Card> computerWarCards)
        {
            Console.WriteLine("");
            Console.WriteLine("You have won the WAR!");

            foreach (Card computerWarCard in computerWarCards)
            {
                Console.WriteLine("You have taken the " + StaticGameUtility.TranslateNumberToFaceCard(computerWarCard.CardValue) + " of " + computerWarCard.CardSuit + " from the computer!");
            }

            Console.WriteLine("");
        }

        public static void PrintComputerWinsWar(List<Card> humanWarCards)
        {
            Console.WriteLine("");
            Console.WriteLine("The computer has won the WAR!");

            foreach (Card humanWarCard in humanWarCards)
            {
               Console.WriteLine("The computer has taken the " + StaticGameUtility.TranslateNumberToFaceCard(humanWarCard.CardValue) + " of " + humanWarCard.CardSuit + " from your deck!");
            }

            Console.WriteLine("");
        }

        #endregion

        #region Parameterless Print Functions

        public static void PrintMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------");
            Console.WriteLine("--------------------------");
            Console.WriteLine("------Game Of War---------");
            Console.WriteLine("--------------------------");
            Console.WriteLine("--------------------------");
            Console.WriteLine();

            Console.WriteLine("1) Start a new game.");
            Console.WriteLine("2) Exit the game.");
        }

        public static void PrintExitMessage()
        { 
            Console.WriteLine();
            Console.WriteLine("Thank you for playing the Game Of War!");
            Console.WriteLine();
        }

        public static void PrintInvalidUserInputMessage()
        {
            Console.WriteLine();
            Console.WriteLine("The inputted option was not recognized.  Please select a valid menu option.");
            Console.WriteLine();
        }

        public static void PrintGameBeginning()
        {
            Console.WriteLine("");
            Console.WriteLine("--------------------------");
            Console.WriteLine("--------------------------");
            Console.WriteLine("----The Game has begun----");
            Console.WriteLine("--------------------------");
            Console.WriteLine("--------------------------");
            Console.WriteLine("");
        }

        public static void PrintHumanLoss()
        {
            Console.WriteLine("");
            Console.WriteLine("The computer has taken all of your cards!  You have zero cards left in your hand!");
            Console.WriteLine("");
            Console.WriteLine("You have lost the game of war.  The computer reigns victorious!");

        }

        public static void PrintHumanWin()
        {
            Console.WriteLine("");
            Console.WriteLine("You have seized all of the computer's cards! The computer has zero cards left in its hand!");
            Console.WriteLine("");
            Console.WriteLine("You have won the game of war. You reign victorious!");
        }

        public static void PrintHumanWarForfeit()
        {
            Console.WriteLine("");
            Console.WriteLine("You have run out of cards during a WAR!  You are unable to continue fighting, and have lost the game!");
            Console.WriteLine("");
        }

        public static void PrintComputerWarForfeit()
        {
            Console.WriteLine("");
            Console.WriteLine("The computer has run out of cards during a WAR!  It is unable to continue fighting, and has lost the game!");
            Console.WriteLine("");
            Console.WriteLine("Congratulations, you are victorious!");
            Console.WriteLine("");
        }

        public static void PrintWarFlavorText()
        {
            Console.WriteLine("");
            Console.WriteLine("The card values are tied!  W-A-R has begun!");
            Console.WriteLine("Both yourself and the computer lay down three additional cards face down, and prepare to play your final card.");
            Console.WriteLine("");
        }

        public static void PrintGameMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("-----");
            Console.WriteLine("1) Play your top card");
            Console.WriteLine("2) Check number of remaining cards");
            Console.WriteLine("-----");
            Console.WriteLine("");
        }

        public static void PrintWarTie()
        {
            Console.Write("The current War has ended in a TIE!  Another War ensues!");
        }

        #endregion
    }
}
