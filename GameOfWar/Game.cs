using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfWar
{
    class Game
    {
        private const int FIRST_CARD_IN_DECK = 0;

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

                int.TryParse(Console.ReadLine(), out menuChoice);

                switch (menuChoice)
                {
                    case 1:
                        PlayCard(humanPlayer, computerPlayer);
                        break;
                    case 2:
                        PrintRemainingHumanCards(humanPlayer);
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("The inputted option was not recognized.  Please select a valid menu option.");
                        Console.WriteLine();
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

            //EnterWarPhase(humanPlayer, computerPlayer);

            
            //The Players have played two cards with the same value.  Begin "WAR" sequence.
            if (humanPlayer.playerCards.First().CardValue == computerPlayer.playerCards.First().CardValue)
            {
                 EnterWarPhase(humanPlayer, computerPlayer);
            }
            else if (humanPlayer.playerCards.First().CardValue > computerPlayer.playerCards.First().CardValue)
            {
                //Print what occurred in the round, and swap the cards between players.
                HumanWinsRound(humanPlayer, computerPlayer);
            }
            else  // No Tie and the Human did not win.  Therefore the computer wins. Give the card to the computer and remove it from the human.
            {
                ComputerWinsRound(humanPlayer, computerPlayer);
            }
            
        }

        private void EnterWarPhase(Player humanPlayer, Player computerPlayer)
        {
            //WAR Logic

            List<Card> humanWarCards = new List<Card>();
            List<Card> computerWarCards = new List<Card>();

            //Add the card the human already played, the additional three cards played for WAR, and the 5th played card that determines the war result.
            humanWarCards.AddRange(humanPlayer.playerCards.GetRange(0, 5));

            //Remove the same 5 cards from the front of the human's deck.
            humanPlayer.playerCards.RemoveRange(0, 5);

            //Same for the computer player.
            computerWarCards.AddRange(computerPlayer.playerCards.GetRange(0, 5));

            computerPlayer.playerCards.RemoveRange(0, 5);

            //Compare the last card in each player's war list.

            PrintWarFinalCard(humanWarCards.Last(), computerWarCards.Last());

            if (humanWarCards.Last().CardValue > computerWarCards.Last().CardValue)
            {
                //The Human has won war.  Add both war card lists to the back of the human deck.
                humanPlayer.playerCards.AddRange(humanWarCards);
                humanPlayer.playerCards.AddRange(computerWarCards);

                Console.WriteLine("");
                Console.WriteLine("You have won the WAR!");

                foreach (Card computerWarCard in computerWarCards)
                {
                    Console.WriteLine("You have taken the " + computerWarCard.CardValue + " of " + computerWarCard.CardSuit + " from the computer!");
                }

                Console.WriteLine("");

            }
            else  //The Computer has won.
            {
                computerPlayer.playerCards.AddRange(computerWarCards);
                computerPlayer.playerCards.AddRange(humanWarCards);

                Console.WriteLine("");
                Console.WriteLine("The computer has won the WAR!");

                foreach (Card humanWarCard in humanWarCards)
                {
                    Console.WriteLine("The computer has taken the " + humanWarCard.CardValue + " of " + humanWarCard.CardSuit + " from your deck!");
                }

                Console.WriteLine("");
            }

        }

        private void ComputerWinsRound(Player humanPlayer, Player computerPlayer)
        {
            PrintRoundWinComputer(humanPlayer);

            //The computer gains a card.  Place it into the back of the computer deck.
            computerPlayer.playerCards.Add(humanPlayer.playerCards.First());

            //Move the winning played card that was played by the computer to the back of the deck.
            Card winningCard = computerPlayer.playerCards.First();
            computerPlayer.playerCards.RemoveAt(FIRST_CARD_IN_DECK);
            computerPlayer.playerCards.Add(winningCard);

            //Remove the losing card that the human played from the huamn's deck.
            humanPlayer.playerCards.RemoveAt(FIRST_CARD_IN_DECK);
        }

        private void HumanWinsRound(Player humanPlayer, Player computerPlayer)
        {
            PrintRoundWinHuman(computerPlayer);

            //The human gains a card.  Place it into the back of the human deck.
            humanPlayer.playerCards.Add(computerPlayer.playerCards.First());

            //Move the winning played card that was played by the human to the back of the deck.
            Card winningCard = humanPlayer.playerCards.First();
            humanPlayer.playerCards.RemoveAt(FIRST_CARD_IN_DECK);
            humanPlayer.playerCards.Add(winningCard);

            //Remove the losing card that the computer played from the computer's deck.
            computerPlayer.playerCards.RemoveAt(FIRST_CARD_IN_DECK);
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

        private void PrintWarFinalCard(Card humanWarCard, Card computerWarCard)
        {
            Console.WriteLine("");
            Console.WriteLine("Your final war card is " + humanWarCard.CardValue + " of " + humanWarCard.CardSuit + "!");
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("The computer's war card is " + computerWarCard.CardValue + " of " + computerWarCard.CardSuit + "!");
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
