namespace GameOfWar
{
    //The Card class represents a single playing card within a Deck.
    public class Card
    {
        public string CardSuit { get; set; }

        public int CardValue { get; set; }

        public Card(string theCardSuit, int theCardValue)
        {
            CardSuit = theCardSuit;
            CardValue = theCardValue;
        }

    }
}
