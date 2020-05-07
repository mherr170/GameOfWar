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
                Console.WriteLine();
                Console.WriteLine("--------------------------");
                Console.WriteLine("--------------------------");
                Console.WriteLine("------Game Of War---------");
                Console.WriteLine("--------------------------");
                Console.WriteLine("--------------------------");
                Console.WriteLine();

                Console.WriteLine("1) Create and print a deck of cards.");
                Console.WriteLine("2) Start a new game.");
                Console.WriteLine("2) Exit the game.");
                Console.WriteLine();

                //Pause the Console from closing
                menuChoice = Convert.ToInt32(Console.ReadLine());

                //replace integers with consts representing menu options.
                switch (menuChoice)
                {
                    case 1:
                        //This option is to ensure that your deck has the proper amount of cards.
                        Deck testDeck = new Deck();
                        testDeck.PrintDeck();
                        break;
                    case 2:
                        //Play the game
                        Game newGame = new Game();
                        break;
                    case 3:
                        //Delay the closing of the console so the end user can see the farewell message.
                        closeAppTimer.Elapsed += CloseAppTimer_Elapsed;
                        closeAppTimer.Start();

                        Console.WriteLine();
                        Console.WriteLine("Thank you for playing the Game Of War!");
                        Console.WriteLine();
                        
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("The inputted option was not recognized.  Please select a valid menu option.");
                        Console.WriteLine();
                        break;
                }
            }       
        }

        private static void CloseAppTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Environment.Exit(SUCCESSFUL_USER_EXIT);
        }
    }
}
