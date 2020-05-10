using System;
using System.Timers;
using GameOfWar.GameLogic;

namespace GameOfWar
{
    public class War
    {
        private const int START_GAME = 1;
        private const int EXIT = 2;
        private const int SUCCESSFUL_USER_EXIT = 0;
        private const int CLOSE_PROGRAM_TIMER_VALUE = 3000;

        static void Main()
        {
            int menuChoice = 0;          

            while (menuChoice != EXIT)
            {
                PrintMainMenu();

                //Not explicitly necessary to evaluate the TryParse result due the the out parameter.
                //However, recording the parse result regardless in the event that further action needs to be taken upon failure.
                bool userInputParseSuccess = int.TryParse(Console.ReadLine(), out menuChoice);
                
                if (!userInputParseSuccess) 
                {
                    menuChoice = 0;
                }

                //replace integers with consts representing menu options.
                switch (menuChoice)
                {
                    case START_GAME:
                        //Play the game
                        Game newGame = new Game();
                        break;
                    case EXIT:
                        ExitGame();
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("The inputted option was not recognized.  Please select a valid menu option.");
                        Console.WriteLine();
                        break;
                }
            }
        }

        private static void PrintMainMenu()
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

        private static void ExitGame()
        {
            Timer closeAppTimer = new Timer(CLOSE_PROGRAM_TIMER_VALUE);

            //Delay the closing of the console so the end user can see the farewell message.
            closeAppTimer.Elapsed += CloseAppTimer_Elapsed;
            closeAppTimer.Start();

            Console.WriteLine();
            Console.WriteLine("Thank you for playing the Game Of War!");
            Console.WriteLine();

            Console.ReadKey();
        }

        private static void CloseAppTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Environment.Exit(SUCCESSFUL_USER_EXIT);
        }
    }
}
