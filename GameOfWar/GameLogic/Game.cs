using System;
using System.Collections.Generic;
using System.Linq;
using GameOfWar.Enums;
using GameOfWar.DTO;

namespace GameOfWar.GameLogic
{
    public class Game
    {
        private const int FIRST_CARD_IN_DECK = 0;
        private const int CARDS_NEEDED_FOR_NORMAL_WAR = 5;
        private const int PLAY_CARD = 1;
        private const int PRINT_NUMBER_OF_REMAINING_CARDS = 2;

        public Game()
        {
            Deck gameDeck = new Deck();

            Player humanPlayer = new Player();
            Player computerPlayer = new Player();

            ShuffleAndDistributeCards(gameDeck, humanPlayer, computerPlayer);

            //Both players now have the necessary cards, the game can begin.
            StartGame(humanPlayer, computerPlayer);
        }

        private void StartGame(Player humanPlayer, Player computerPlayer)
        {
            int gameState = (int)GameState.GAME_CONTINUES;

            PrintGameBeginning();

            while (gameState == (int)GameState.GAME_CONTINUES)
            {
                PrintGameMenu();

                //Recording the parse result regardless in the event that further action needs to be taken upon failure.
                bool userInputParseSuccess = int.TryParse(Console.ReadLine(), out int menuChoice);

                if (!userInputParseSuccess)
                {
                    menuChoice = 0;
                }

                switch (menuChoice)
                {
                    case PLAY_CARD:
                        gameState = PlayCard(humanPlayer, computerPlayer);
                        break;
                    case PRINT_NUMBER_OF_REMAINING_CARDS:
                        PrintRemainingHumanCards(humanPlayer);
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("The inputted option was not recognized.  Please select a valid menu option.");
                        Console.WriteLine();
                        break;
                }

            }

            switch (gameState)
            {
                case (int)GameState.HUMAN_LOSS:
                    PrintHumanLoss();
                    break;
                case (int)GameState.COMPUTER_LOSS:
                    PrintHumanWin();
                    break;
                case (int)GameState.WARFORFEIT_HUMAN:
                    PrintHumanWarForfeit();
                    break;
                case (int)GameState.WARFORFEIT_COMPUTER:
                    PrintComputerWarForfeit();
                    break;
            }

        }

        private int PlayCard(Player humanPlayer, Player computerPlayer)
        {
            //If either player has run out of cards during a WAR, signal that the game has been forfeit.
            bool humanWarForfeit = false;
            bool computerWarForfeit = false;

            PrintCurrentMove(humanPlayer, computerPlayer);

            //The Players have played two cards with the same value.  Begin "WAR" sequence.
            if (humanPlayer.PlayerCards.First().CardValue == computerPlayer.PlayerCards.First().CardValue)
            {
                EnterWarPhase(humanPlayer, computerPlayer, ref humanWarForfeit, ref computerWarForfeit);
            }
            else if (humanPlayer.PlayerCards.First().CardValue > computerPlayer.PlayerCards.First().CardValue)
            {
                //Print what occurred in the round, and swap the cards between players.
                HumanWinsRound(humanPlayer, computerPlayer);
            }
            else  // No Tie and the Human did not win.  Therefore the computer wins. Give the card to the computer and remove it from the human.
            {
                ComputerWinsRound(humanPlayer, computerPlayer);
            }

            //check if the game has ended
            int gameState = CheckGameEndingConditions(humanPlayer, computerPlayer, humanWarForfeit, computerWarForfeit);

            return gameState;

        }

        private int CheckGameEndingConditions(Player humanPlayer, Player computerPlayer, bool humanWarForfeit, bool computerWarForfeit)
        {
            if (IsHumanOutOfCards(humanPlayer))
            {
                return (int)GameState.HUMAN_LOSS;
            }
            else if (IsComputerOutOfCards(computerPlayer))
            {
                return (int)GameState.COMPUTER_LOSS;
            }
            else if (humanWarForfeit)
            {
                return (int)GameState.WARFORFEIT_HUMAN;
            }
            else if (computerWarForfeit)
            {
                return (int)GameState.WARFORFEIT_COMPUTER;
            }
            else
            {
                return (int)GameState.GAME_CONTINUES;
            }

        }

        private bool IsHumanOutOfCards(Player humanPlayer)
        {
            return (humanPlayer.PlayerCards.Count == 0);
        }

        private bool IsComputerOutOfCards(Player computerPlayer)
        {
            return (computerPlayer.PlayerCards.Count == 0);
        }

        private void EnterWarPhase(Player humanPlayer, Player computerPlayer, ref bool humanWarForfeit, ref bool computerWarForfeit)
        {
            //Create lists to hold the cards played during WAR phase.
            List<Card> humanWarCards = new List<Card>();
            List<Card> computerWarCards = new List<Card>();
            bool isWarOngoing = true;

            do
            {
                if (humanPlayer.PlayerCards.Count >= CARDS_NEEDED_FOR_NORMAL_WAR && computerPlayer.PlayerCards.Count >= CARDS_NEEDED_FOR_NORMAL_WAR)
                {
                    //Known problem, in the event of more than 1 consecutive war, another 5 cards are at stake instead of just another 4.
                    FiveCardWar(humanPlayer, computerPlayer, humanWarCards, computerWarCards, ref isWarOngoing);
                }
                else
                {
                    //One of the players did not have the cards to fight the war.  Trigger loss condition
                    isWarOngoing = false;

                    if (humanPlayer.PlayerCards.Count < CARDS_NEEDED_FOR_NORMAL_WAR)
                    {
                        humanWarForfeit = true;
                    }
                    else if (computerPlayer.PlayerCards.Count < CARDS_NEEDED_FOR_NORMAL_WAR)
                    {
                        computerWarForfeit = true;
                    }
                }
            }
            while (isWarOngoing);

        }

        private void FiveCardWar(Player humanPlayer, Player computerPlayer, List<Card> humanWarCards, List<Card> computerWarCards, ref bool isWarOngoing)
        {
            //Take the cards at stake from each player during War and store them in the War Card list.
            PopulateWarCards(humanPlayer, computerPlayer, humanWarCards, computerWarCards);

            //Compare the last card in each player's war list.
            PrintWarFinalCard(humanWarCards.Last(), computerWarCards.Last());

            //War has happened more than one time in a row.
            if (humanWarCards.Last().CardValue == computerWarCards.Last().CardValue)
            {
                Console.Write("The current War has ended in a TIE!  Another War ensues!");
            }
            else if (humanWarCards.Last().CardValue > computerWarCards.Last().CardValue)
            {
                HumanWinsWar(humanPlayer, humanWarCards, computerWarCards, ref isWarOngoing);
            }
            else  //The Computer has won.
            {
                ComputerWinsWar(computerPlayer, humanWarCards, computerWarCards, ref isWarOngoing);
            }
        }

        private void HumanWinsWar(Player humanPlayer, List<Card> humanWarCards, List<Card> computerWarCards, ref bool isWarOngoing)
        {
            isWarOngoing = false;

            //The Human has won the war.  Add both war card lists to the back of the human deck.
            humanPlayer.PlayerCards.AddRange(humanWarCards);
            humanPlayer.PlayerCards.AddRange(computerWarCards);

            Console.WriteLine("");
            Console.WriteLine("You have won the WAR!");

            foreach (Card computerWarCard in computerWarCards)
            {
                Console.WriteLine("You have taken the " + TranslateNumberToFaceCard(computerWarCard.CardValue) + " of " + computerWarCard.CardSuit + " from the computer!");
            }

            Console.WriteLine("");
        }

        private void ComputerWinsWar(Player computerPlayer, List<Card> humanWarCards, List<Card> computerWarCards, ref bool isWarOngoing)
        {
            isWarOngoing = false;

            //The Computer has won the war.   Add both war card lists to the back of the computer deck.
            computerPlayer.PlayerCards.AddRange(computerWarCards);
            computerPlayer.PlayerCards.AddRange(humanWarCards);

            Console.WriteLine("");
            Console.WriteLine("The computer has won the WAR!");

            foreach (Card humanWarCard in humanWarCards)
            {
                Console.WriteLine("The computer has taken the " + TranslateNumberToFaceCard(humanWarCard.CardValue) + " of " + humanWarCard.CardSuit + " from your deck!");
            }

            Console.WriteLine("");
        }

        private void PopulateWarCards(Player humanPlayer, Player computerPlayer, List<Card> humanWarCards, List<Card> computerWarCards)
        {
            //Add the card the human already played, the additional three cards played for WAR, and the 5th played card that determines the war result.
            humanWarCards.AddRange(humanPlayer.PlayerCards.GetRange(0, CARDS_NEEDED_FOR_NORMAL_WAR));

            //Remove the same 5 cards from the front of the human's deck.
            humanPlayer.PlayerCards.RemoveRange(0, CARDS_NEEDED_FOR_NORMAL_WAR);

            //Same for the computer player.
            computerWarCards.AddRange(computerPlayer.PlayerCards.GetRange(0, CARDS_NEEDED_FOR_NORMAL_WAR));

            computerPlayer.PlayerCards.RemoveRange(0, CARDS_NEEDED_FOR_NORMAL_WAR);
        }

        private void ComputerWinsRound(Player humanPlayer, Player computerPlayer)
        {
            PrintRoundWinComputer(humanPlayer);

            //The computer gains a card.  Place it into the back of the computer deck.
            computerPlayer.PlayerCards.Add(humanPlayer.PlayerCards.First());

            //Move the winning played card that was played by the computer to the back of the deck.
            Card winningCard = computerPlayer.PlayerCards.First();
            computerPlayer.PlayerCards.RemoveAt(FIRST_CARD_IN_DECK);
            computerPlayer.PlayerCards.Add(winningCard);

            //Remove the losing card that the human played from the huamn's deck.
            humanPlayer.PlayerCards.RemoveAt(FIRST_CARD_IN_DECK);
        }

        private void HumanWinsRound(Player humanPlayer, Player computerPlayer)
        {
            PrintRoundWinHuman(computerPlayer);

            //The human gains a card.  Place it into the back of the human deck.
            humanPlayer.PlayerCards.Add(computerPlayer.PlayerCards.First());

            //Move the winning played card that was played by the human to the back of the deck.
            Card winningCard = humanPlayer.PlayerCards.First();
            humanPlayer.PlayerCards.RemoveAt(FIRST_CARD_IN_DECK);
            humanPlayer.PlayerCards.Add(winningCard);

            //Remove the losing card that the computer played from the computer's deck.
            computerPlayer.PlayerCards.RemoveAt(FIRST_CARD_IN_DECK);
        }

        private string TranslateNumberToFaceCard(int cardValue)
        {
            switch (cardValue)
            {
                case (int)CardValues.Jack:
                    return CardValues.Jack.ToString();
                case (int)CardValues.Queen:
                    return CardValues.Queen.ToString();
                case (int)CardValues.King:
                    return CardValues.King.ToString();
                case (int)CardValues.Ace:
                    return CardValues.Ace.ToString();
                default:
                    return cardValue.ToString();
            }
        }

        private void ShuffleAndDistributeCards(Deck gameDeck, Player humanPlayer, Player computerPlayer)
        {
            Random r = new Random();

            int num;

            //Loop decrements by two because one iteration deals out two cards.
            for (int cardsRemainingInDeck = gameDeck.DeckOfCards.Count; cardsRemainingInDeck > 0; cardsRemainingInDeck -= 2)
            {
                //Generate a random number from 0 to one less than the amount of cards remaining in the deck.
                //It is one less because Random.Next function will return a number from 0 up to but not including the specified maximum.
                num = r.Next(cardsRemainingInDeck - 1);

                //Take that random number, and assign the card that associates with it to the Human Player.
                humanPlayer.PlayerCards.Add(gameDeck.DeckOfCards[num]);

                //Remove the card you just gave to the Human Player from the gameDeck
                gameDeck.DeckOfCards.RemoveAt(num);

                //Generate Random number from 0 to 2 less than the cards remaining, because one card has already been removed and given it to the Human.
                num = r.Next(cardsRemainingInDeck - 2);

                //Assign the next card to the computer player.
                computerPlayer.PlayerCards.Add(gameDeck.DeckOfCards[num]);

                //Remove the card given to the computer player from the deck.
                gameDeck.DeckOfCards.RemoveAt(num);
            }
        }

        //A future improvement should be to move the print functionality out of the game logic class.
        #region PRINT FUNCTIONS

        private void PrintHumanWarForfeit()
        {
            Console.WriteLine("");
            Console.WriteLine("You have run out of cards during a WAR!  You are unable to continue fighting, and have lost the game!");
            Console.WriteLine("");
        }

        private void PrintComputerWarForfeit()
        {
            Console.WriteLine("");
            Console.WriteLine("The computer has run out of cards during a WAR!  It is unable to continue fighting, and has lost the game!");
            Console.WriteLine("");
            Console.WriteLine("Congratulations, you are victorious!");
            Console.WriteLine("");
        }


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
            Console.WriteLine("You have placed your card and the " + TranslateNumberToFaceCard(computerPlayer.PlayerCards.First().CardValue) + " of " + computerPlayer.PlayerCards.First().CardSuit + " into the bottom of your hand.");
            Console.WriteLine("");
        }

        private void PrintRoundWinComputer(Player humanPlayer)
        {
            Console.WriteLine("");
            Console.WriteLine("You lost this round!");
            Console.WriteLine("The computer has taken its card and the " + TranslateNumberToFaceCard(humanPlayer.PlayerCards.First().CardValue) + " of " + humanPlayer.PlayerCards.First().CardSuit + " and placed it into the bottom of its hand.");
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
            Console.WriteLine("You play the " + TranslateNumberToFaceCard(humanPlayer.PlayerCards.First().CardValue) + " of " + humanPlayer.PlayerCards.First().CardSuit + "!");
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("The computer plays the " + TranslateNumberToFaceCard(computerPlayer.PlayerCards.First().CardValue) + " of " + computerPlayer.PlayerCards.First().CardSuit + "!");
            Console.WriteLine("");
        }

        private void PrintWarFinalCard(Card humanWarCard, Card computerWarCard)
        {
            PrintWarFlavorText();

            Console.WriteLine("");
            Console.WriteLine("Your final war card is the " + TranslateNumberToFaceCard(humanWarCard.CardValue) + " of " + humanWarCard.CardSuit + "!");
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("The computer's war card is the " + TranslateNumberToFaceCard(computerWarCard.CardValue) + " of " + computerWarCard.CardSuit + "!");
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
            Console.WriteLine("You have " + humanPlayer.PlayerCards.Count + " cards left in your hand.");
            Console.WriteLine("");
        }
        #endregion

    }
}
