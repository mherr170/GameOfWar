using System;

namespace GameOfWar.PrintLogic.PrintStatic
{
    static class PrintStatic
    {
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

    }
}
