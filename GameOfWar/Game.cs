using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfWar
{
    class Game
    {

        public Game()
        {
            Deck gameDeck = new Deck();

            Player humanPlayer = new Player();
            Player computerPlayer = new Player();
            computerPlayer.isComputerPlayer = true;

            ShuffleAndDistributeCards(gameDeck, humanPlayer, computerPlayer);

            //Both players now have the necessary cards, the game can begin.
            StartGame(humanPlayer, computerPlayer);
        }

        private void StartGame(Player humanPlayer, Player computerPlayer)
        {
            int menuChoice;

            PrintGameBeginning();

            //while neither player has 0 cards...

            while (humanPlayer.playerCards.Count > 0 && computerPlayer.playerCards.Count > 0)
            {
                PrintGameMenu();

                //pressing ENTER here will break it.  Need a try/catch probably
                menuChoice = Convert.ToInt32(Console.ReadLine());

                switch(menuChoice)
                {
                    case 1:
                        PlayCard(humanPlayer, computerPlayer);
                        break;
                    case 2:
                        PrintRemainingHumanCards(humanPlayer);
                        break;
                    default:
                        break;
                }

            }
        }

        private void PrintRemainingHumanCards(Player humanPlayer)
        {
            Console.WriteLine("");
            Console.WriteLine("You have " + humanPlayer.playerCards.Count + " left in your hand.");
            Console.WriteLine("");
        }

        private void PlayCard(Player humanPlayer, Player computerPlayer)
        {
            PrintCurrentMove(humanPlayer, computerPlayer);         

            if (humanPlayer.playerCards.First().CardValue > computerPlayer.playerCards.First().CardValue)
            {
                PrintRoundWinHuman(computerPlayer);

                //the human wins, give the card to the human and remove it from the computer.


                //Add the newly acquired card to the back of the deck.  
                humanPlayer.playerCards.Add(computerPlayer.playerCards.First());

                //Move your current first card to the BACK of the deck.
                Card temp = humanPlayer.playerCards.First();
                humanPlayer.playerCards.RemoveAt(0);
                humanPlayer.playerCards.Add(temp);

                computerPlayer.playerCards.RemoveAt(0);

            }
            else  // The computer wins, give the card to the computer and remove it from the human.
            {
                PrintRoundWinComputer(humanPlayer);

                computerPlayer.playerCards.Add(humanPlayer.playerCards.First());

                Card temp = computerPlayer.playerCards.First();
                computerPlayer.playerCards.RemoveAt(0);
                computerPlayer.playerCards.Add(temp);

                humanPlayer.playerCards.RemoveAt(0);
            }

            // TO DO -  DEAL WITH TIES 
        }

        private void PrintRoundWinComputer(Player humanPlayer)
        {
            Console.WriteLine("");
            Console.WriteLine("You lost this round!");
            Console.WriteLine("The computer has taken the " + humanPlayer.playerCards.First().CardValue + " of " + humanPlayer.playerCards.First().CardSuit + " and placed it into the bottom of its hand.");
            Console.WriteLine("");
        }

        private void PrintRoundWinHuman(Player computerPlayer)
        {
            Console.WriteLine("");
            Console.WriteLine("You won this round!");
            Console.WriteLine("You have placed the " + computerPlayer.playerCards.First().CardValue + " of " + computerPlayer.playerCards.First().CardSuit + " into the bottom of your hand.");
            Console.WriteLine("");
        }

        private void PrintGameMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("-----");
            Console.WriteLine("1) Play your top card");
            Console.WriteLine("2) Check number of remaining cards");
            Console.WriteLine("-----");
            Console.WriteLine("");
        }

        private void PrintCurrentMove(Player humanPlayer, Player computerPlayer)
        {
            Console.WriteLine("");
            Console.WriteLine("You play the " + humanPlayer.playerCards.First().CardValue + " of " + humanPlayer.playerCards.First().CardSuit + "!");
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("The computer plays the " + computerPlayer.playerCards.First().CardValue + " of " + computerPlayer.playerCards.First().CardSuit + "!");
            Console.WriteLine("");
        }

        private void PrintGameBeginning()
        {
            Console.WriteLine("");
            Console.WriteLine("--------------------------");
            Console.WriteLine("--------------------------");
            Console.WriteLine("----The Game has begun----");
            Console.WriteLine("--------------------------");
            Console.WriteLine("--------------------------");
            Console.WriteLine("");
        }

        private void ShuffleAndDistributeCards(Deck gameDeck, Player humanPlayer, Player computerPlayer)
        {
            Random r = new Random();

            int num;

            //Loop decrements by two because one iteration deals out two cards.
            for (int cardsRemainingInDeck = 52; cardsRemainingInDeck > 0; cardsRemainingInDeck -= 2 )
            {
                //Generate a random number from 0 to one less than the amount of cards remaining in the deck.
                //It is one less because Random.Next function will return a number from 0 up to but not including the specified maximum.
                num = r.Next(cardsRemainingInDeck - 1);

                //Take that random number, and assign the card that associates with it to the Human Player.
                humanPlayer.playerCards.Add(gameDeck.deckOfCards[num]);

                //Console.WriteLine("The " + gameDeck.deckOfCards[num].CardValue + " of " + gameDeck.deckOfCards[num].CardSuit + " has been given to the Human Player.");

                //Remove the card you just gave to the Human Player from the gameDeck
                gameDeck.deckOfCards.RemoveAt(num);

                //Generate Random number from 0 to 2 less than the cards remaining, because one card has already been removed and given it to the Human.
                num = r.Next(cardsRemainingInDeck - 2);

                //Assign the next card to the computer player.
                computerPlayer.playerCards.Add(gameDeck.deckOfCards[num]);

                //Console.WriteLine("The " + gameDeck.deckOfCards[num].CardValue + " of " + gameDeck.deckOfCards[num].CardSuit + " has been given to the Computer Player.");

                //Remove the card given to the computer player from the deck.
                gameDeck.deckOfCards.RemoveAt(num);

            }

            //humanPlayer.PrintPlayerHand();
            //computerPlayer.PrintPlayerHand();
        }

    }
}
