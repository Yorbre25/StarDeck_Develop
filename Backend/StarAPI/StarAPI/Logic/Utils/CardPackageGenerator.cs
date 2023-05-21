using System;
using System.Security.Cryptography;
using System.Text;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Context;

namespace StarAPI.Logic
{
    public class CardPackageGenerator
    {
        private CardHandling _cardHandling;

        private static int s_cardsPerPackage = 3;
        private static int s_packagedOfCards = 3;
        public CardPackageGenerator(StarDeckContext _context)
        {
            this._cardHandling = new CardHandling(_context);
        }



        // Top public method for getting packages of cards
        public List<List<OutputCard>> GetPackagesForNewPlayer()
        {
            try
            {
                return GettingPackages();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private List<List<OutputCard>> GettingPackages()
        {
            List<OutputCard> suitableCards = GetCardsSuitableForPackage();
            List<OutputCard> cardsToSend = GetCardsToSend(suitableCards);
            return BuildPackages(cardsToSend);
        }

        // Return a card list of type "Normal" or "Rara"
        private List<OutputCard> GetCardsSuitableForPackage ()
        {
            string cardTypeName1 = "Rara";
            string cardTypeName2 = "Normal";
            List<OutputCard> rareCards = GetCardsWith(cardTypeName1);
            List<OutputCard> normalCards = GetCardsWith(cardTypeName2);
            return JoinListOfCards(rareCards, normalCards);
        }

        private List<OutputCard> GetCardsToSend(List<OutputCard> suitableCards)
        {
            bool enoughCards = EnoughCards(suitableCards.Count());
            if (!enoughCards)
            {
                throw new Exception("Not enough cards");
            }
            return GettingCardsToSend(suitableCards);
        }

        private List<OutputCard> GettingCardsToSend(List<OutputCard> suitableCards)
        {
            List<OutputCard> selectedCards = new List<OutputCard>();
            for (int i = 0; i < s_packagedOfCards * s_cardsPerPackage; i++)
            {
                OutputCard card = GetRandomCard(suitableCards);
                selectedCards.Add(card);
                suitableCards.Remove(card);
            }
            return selectedCards;
        }

        private List<List<OutputCard>> BuildPackages(List<OutputCard> cardToSend)
        {
            List<List<OutputCard>> packages = new List<List<OutputCard>>();
            for (int i = 0; i < s_packagedOfCards; i++)
            {
                List<OutputCard> package = new List<OutputCard>();
                for (int j = 0; j < s_cardsPerPackage; j++)
                {
                    OutputCard card = GetRandomCard(cardToSend);
                    package.Add(card);
                    cardToSend.Remove(card);
                }
                packages.Add(package);
            }
            return packages;
        }

        public List<OutputCard> JoinListOfCards(List<OutputCard> cards1, List<OutputCard> cards2){
            List<OutputCard> allCards;
            allCards = cards1.Union(cards2).ToList();
            return allCards;
        }

        private OutputCard GetRandomCard(List<OutputCard> cards)
        {
            Random random = new Random();
            int index = random.Next(cards.Count);
            return cards[index];
        }

        public bool EnoughCards(int numberOfCards)
        {
            if (numberOfCards < s_cardsPerPackage * s_packagedOfCards)
            {
                return false;
            }
            return true;
            
        }

        private List<OutputCard> GetCardsWith(string cardTypeName)
        {
            List<OutputCard> allCards = _cardHandling.GetAllCards();
            List<OutputCard> specificCards;
            specificCards = allCards.Where(c => c.type == cardTypeName).ToList();
            return specificCards;
        }

    }
}
