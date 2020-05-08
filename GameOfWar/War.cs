using System;
using System.Timers;

namespace GameOfWar
{
    class War
    {
        private const int EXIT = 2;
        private const int SUCCESSFUL_USER_EXIT = 0;
        private const int CLOSE_PROGRAM_TIMER_VALUE = 3000;

        static void Main(string[] args)
        {
            int menuChoice = 0;

            Timer closeAppTimer = new Timer(CLOSE_PROGRAM_TIMER_VALUE);

            while (menuChoice != EXIT)
            {
                PrintMainMenu();

                //No need to evaluate the TryParse result, menuChoice will handle it in the switch statement.
                int.TryParse(Console.ReadLine(), out menuChoice);
             
                //replace integers with consts representing menu options.
                switch (menuChoice)
                {
                    case 1:
                        //Play the game
                        Game newGame = new Game();
                        break;
                    case 2:
                        //Delay the closing of the console so the end user can see the farewell message.
                        closeAppTimer.Elapsed += CloseAppTimer_Elapsed;
                        closeAppTimer.Start();

                        Console.WriteLine();
                        Console.WriteLine("Thank you for playing the Game Of War!");
                        Console.WriteLine();
                        
                        Console.ReadKey();
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

        private static void CloseAppTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Environment.Exit(SUCCESSFUL_USER_EXIT);
        }
    }
}
