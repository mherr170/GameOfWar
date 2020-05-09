using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace GameOfWar
{
    class Game
    {
        private const int FIRST_CARD_IN_DECK = 0;
        private const string JACK = "Jack";
        private const string QUEEN = "Queen";
        private const string KING = "King";
        private const string ACE = "Ace";
        private const int CARDS_NEEDED_FOR_NORMAL_WAR = 5;

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
            PrintGameBeginning();

            //while neither player has 0 cards...
            while (humanPlayer.playerCards.Count > 0 && computerPlayer.playerCards.Count > 0)
            {
                PrintGameMenu();

                int.TryParse(Console.ReadLine(), out int menuChoice);

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

            //If we are outside the loop, one of the players has 0 cards remaining, or the LOSS condition has been met via multiple wars.
            if (humanPlayer.playerCards.Count == 0)
            {
                //The computer has won.
                PrintHumanLoss();  
            }
            else if (computerPlayer.playerCards.Count == 0)
            {
                //the human has won.
                PrintHumanWin();
            }
            //Handle Loss from running out of cards during WAR.

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

            //Create lists to hold the cards played during WAR phase.
            List<Card> humanWarCards = new List<Card>();
            List<Card> computerWarCards = new List<Card>();
            bool isWarOngoing = true;

            do
            {
                if (humanPlayer.playerCards.Count >= CARDS_NEEDED_FOR_NORMAL_WAR && computerPlayer.playerCards.Count >= CARDS_NEEDED_FOR_NORMAL_WAR)
                { 
                    //Known problem, in the event of more than 1 consecutive war, another 5 cards are at stake instead of just another 4.
                    FiveCardWar(humanPlayer, computerPlayer, humanWarCards, computerWarCards, ref isWarOngoing);
                }
                else
                {
                    //One of the players did not have the cards to fight the war.  Trigger loss condition
                    //Bubble up loss condition here somehow.
                    isWarOngoing = false;
                }
            }
            while (isWarOngoing);
        }

        private void FiveCardWar(Player humanPlayer, Player computerPlayer, List<Card> humanWarCards, List<Card> computerWarCards, ref bool isWarOngoing)
        {
            #region WAR LIST LOGIC

            //Add the card the human already played, the additional three cards played for WAR, and the 5th played card that determines the war result.
            humanWarCards.AddRange(humanPlayer.playerCards.GetRange(0, CARDS_NEEDED_FOR_NORMAL_WAR));

            //Remove the same 5 cards from the front of the human's deck.
            humanPlayer.playerCards.RemoveRange(0, CARDS_NEEDED_FOR_NORMAL_WAR);

            //Same for the computer player.
            computerWarCards.AddRange(computerPlayer.playerCards.GetRange(0, CARDS_NEEDED_FOR_NORMAL_WAR));

            computerPlayer.playerCards.RemoveRange(0, CARDS_NEEDED_FOR_NORMAL_WAR);

            #endregion

            //Compare the last card in each player's war list.
            PrintWarFinalCard(humanWarCards.Last(), computerWarCards.Last());

            //War has happened more than one time in a row.
            if (humanWarCards.Last().CardValue == computerWarCards.Last().CardValue)
            {
                Console.Write("Two or more Wars in a row");
            }
            else if (humanWarCards.Last().CardValue > computerWarCards.Last().CardValue)
            {
                isWarOngoing = false;

                //The Human has won war.  Add both war card lists to the back of the human deck.
                humanPlayer.playerCards.AddRange(humanWarCards);
                humanPlayer.playerCards.AddRange(computerWarCards);

                Console.WriteLine("");
                Console.WriteLine("You have won the WAR!");

                foreach (Card computerWarCard in computerWarCards)
                {
                    Console.WriteLine("You have taken the " + TranslateNumberToFaceCard(computerWarCard.CardValue) + " of " + computerWarCard.CardSuit + " from the computer!");
                }

                Console.WriteLine("");

            }
            else  //The Computer has won.
            {
                isWarOngoing = false;

                computerPlayer.playerCards.AddRange(computerWarCards);
                computerPlayer.playerCards.AddRange(humanWarCards);

                Console.WriteLine("");
                Console.WriteLine("The computer has won the WAR!");

                foreach (Card humanWarCard in humanWarCards)
                {
                    Console.WriteLine("The computer has taken the " + TranslateNumberToFaceCard(humanWarCard.CardValue) + " of " + humanWarCard.CardSuit + " from your deck!");
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

        private string TranslateNumberToFaceCard(int cardValue)
        {
            switch (cardValue)
            {
                case (int)CardValues.Jack:
                    return JACK;
                case (int)CardValues.Queen:
                    return QUEEN;
                case (int)CardValues.King:
                    return KING;
                case (int)CardValues.Ace:
                    return ACE;
                default:
                    return cardValue.ToString();
            }
        }

        private void ShuffleAndDistributeCards(Deck gameDeck, Player humanPlayer, Player computerPlayer)
        {
            Random r = new Random();

            int num;

            //Loop decrements by two because one iteration deals out two cards.
            for (int cardsRemainingInDeck = 52; cardsRemainingInDeck > 0; cardsRemainingInDeck -= 2)
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

        #region PRINT FUNCTIONS

        private void PrintHumanLoss()
        {
            Console.WriteLine("");
            Console.WriteLine("The computer has taken all of your cards!  You have zero cards left in your hand!");
            Console.WriteLine("");
            Console.WriteLine("You have lost the game of war.  The computer reigns victorious!");

        }

        private void PrintHumanWin()
        {
            Console.WriteLine("");
            Console.WriteLine("You have seized all of the computer's cards! The computer has zero cards left in its hand!");
            Console.WriteLine("");
            Console.WriteLine("You have won the game of war. You reign victorious!");
        }

        private void PrintRoundWinHuman(Player computerPlayer)
        {
            Console.WriteLine("");
            Console.WriteLine("You won this round!");
            Console.WriteLine("You have placed the " + TranslateNumberToFaceCard(computerPlayer.playerCards.First().CardValue) + " of " + computerPlayer.playerCards.First().CardSuit + " into the bottom of your hand.");
            Console.WriteLine("");
        }

        private void PrintRoundWinComputer(Player humanPlayer)
        {
            Console.WriteLine("");
            Console.WriteLine("You lost this round!");
            Console.WriteLine("The computer has taken the " + TranslateNumberToFaceCard(humanPlayer.playerCards.First().CardValue) + " of " + humanPlayer.playerCards.First().CardSuit + " and placed it into the bottom of its hand.");
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
            Console.WriteLine("You play the " + TranslateNumberToFaceCard(humanPlayer.playerCards.First().CardValue) + " of " + humanPlayer.playerCards.First().CardSuit + "!");
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("The computer plays the " + TranslateNumberToFaceCard(computerPlayer.playerCards.First().CardValue) + " of " + computerPlayer.playerCards.First().CardSuit + "!");
            Console.WriteLine("");
        }

        private void PrintWarFinalCard(Card humanWarCard, Card computerWarCard)
        {
            PrintWarFlavorText();

            Console.WriteLine("");
            Console.WriteLine("Your final war card is " + TranslateNumberToFaceCard(humanWarCard.CardValue) + " of " + humanWarCard.CardSuit + "!");
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("The computer's war card is " + TranslateNumberToFaceCard(computerWarCard.CardValue) + " of " + computerWarCard.CardSuit + "!");
            Console.WriteLine("");
        }

        private void PrintWarFlavorText()
        {
            Console.WriteLine("");
            Console.WriteLine("The card values are tied!  W-A-R has begun!");
            Console.WriteLine("Both yourself and the computer lay down three additional cards face down, and prepare to play your final card.");
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

        private void PrintRemainingHumanCards(Player humanPlayer)
        {
            Console.WriteLine("");
            Console.WriteLine("You have " + humanPlayer.playerCards.Count + " cards left in your hand.");
            Console.WriteLine("");
        }
        #endregion
        
    }
}
