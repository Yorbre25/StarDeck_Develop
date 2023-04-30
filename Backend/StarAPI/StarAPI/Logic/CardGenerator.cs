using System;
using System.Security.Cryptography;
using System.Text;
using StarAPI.DTOs;
using StarAPI.Logic.AdminLogic;
using StarAPI.Context;

namespace StarAPI.Logic
{
    public class CardGenerator
    {
        private CardHandling _cardHandling;

        private static int s_cardsPerPackage = 3;
        private static int s_packagedOfCards = 3;
        public CardGenerator(StarDeckContext _context)
        {
            this._cardHandling = new CardHandling(_context);
        }

        public List<OutputCard> getCardsWith(string cardTypeName)
        {
            List<OutputCard> allCards = _cardHandling.GetAllCards();
            List<OutputCard> specificCards;
            specificCards = allCards.Where(c => c.type == cardTypeName).ToList();
            return specificCards;
        }

        private OutputCard GetRandomCard(List<OutputCard> cards)
        {
            Random random = new Random();
            int index = random.Next(cards.Count);
            return cards[index];
        }

        public OutputCard GetRandomCardWith(string cardTypeName)
        {
            List<OutputCard> cards = getCardsWith(cardTypeName);
            return GetRandomCard(cards);
        }

        



        public string gen_id(string type)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            var rand_string = new string(Enumerable.Repeat(chars, 12).Select(s => s[random.Next(s.Length)]).ToArray());
            return type+"-"+rand_string;
        }
    }
}
