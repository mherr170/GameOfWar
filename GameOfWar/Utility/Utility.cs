using GameOfWar.Enums;
using GameOfWar.DTO;

namespace GameOfWar.Utility
{
    static class StaticGameUtility
    {
        public static string TranslateNumberToFaceCard(int cardValue)
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

        public static int CheckGameEndingConditions(Player humanPlayer, Player computerPlayer, bool humanWarForfeit, bool computerWarForfeit)
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

        private static bool IsHumanOutOfCards(Player humanPlayer)
        {
            return (humanPlayer.PlayerCards.Count == 0);
        }

        private static bool IsComputerOutOfCards(Player computerPlayer)
        {
            return (computerPlayer.PlayerCards.Count == 0);
        }
    }
}
