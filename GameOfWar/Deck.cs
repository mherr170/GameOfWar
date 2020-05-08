using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace GameOfWar
{
    class Deck
    {
        private const int numberOfCardsInDeck = 52;

        private const int numberOfCardSuits = 4;

        private const int numberOfCardsPerSuit = 13;

        private const string HEARTS = "Hearts";
        private const string DIAMONDS = "Diamonds";
        private const string SPADES = "Spades";
        private const string CLUBS = "Clubs";

        public List<Card> deckOfCards = new List<Card>();

        private List<String> cardSuits = new List<String>();
        private List<int> cardValuesPerSuit = new List<int>();

        enum CardValues
        {
            Zero,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace            
        }

        public Deck()
        {
            //Build card suit list
            BuildSuitList();

            //Build card value list
            BuildCardValueList();

            //Build the Deck, 52 cards.
            BuildDeckOfCards();
        }

        private void BuildSuitList()
        {
            cardSuits.Add(HEARTS);
            cardSuits.Add(DIAMONDS);
            cardSuits.Add(SPADES);
            cardSuits.Add(CLUBS);
        }

        private void BuildCardValueList()
        {
            cardValuesPerSuit.Add((int)CardValues.Two);
            cardValuesPerSuit.Add((int)CardValues.Three);
            cardValuesPerSuit.Add((int)CardValues.Four);
            cardValuesPerSuit.Add((int)CardValues.Five);
            cardValuesPerSuit.Add((int)CardValues.Six);
            cardValuesPerSuit.Add((int)CardValues.Seven);
            cardValuesPerSuit.Add((int)CardValues.Eight);
            cardValuesPerSuit.Add((int)CardValues.Nine);
            cardValuesPerSuit.Add((int)CardValues.Ten);
            cardValuesPerSuit.Add((int)CardValues.Jack);
            cardValuesPerSuit.Add((int)CardValues.Queen);
            cardValuesPerSuit.Add((int)CardValues.King);
            cardValuesPerSuit.Add((int)CardValues.Ace);
        }

        private void BuildDeckOfCards()
        {
            //Suit Loop will run 4 times, once for each suit
            for (int suit = 0; suit < numberOfCardSuits; suit++)
            {
                //Card Loop will run 13 times, per suit.
                for (int cardCount = 0; cardCount < numberOfCardsPerSuit; cardCount++)
                {
                    //Create the Card object inline, and add it to the deck.
                    deckOfCards.Add(new Card(cardSuits[suit], cardValuesPerSuit[cardCount]));
                }
            }
        }

        //Debugging purposes - ensure that a deck received the correct amount of cards.
        public void PrintDeck()
        {
            Console.WriteLine("");
            Console.WriteLine("A normal deck has " + numberOfCardsInDeck + " cards.");
            Console.WriteLine("");
            Console.WriteLine("This game deck has " + deckOfCards.Count + " cards.");
            Console.WriteLine("");

            for (int cardCount = 0; cardCount < numberOfCardsInDeck; cardCount++)
            {
                Console.WriteLine(deckOfCards[cardCount].CardValue + " of " + deckOfCards[cardCount].CardSuit);
            }
        }

    }
}
