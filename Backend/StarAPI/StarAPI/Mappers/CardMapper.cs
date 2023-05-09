using StarAPI.Context;
using StarAPI.DTOs;
using StarAPI.Logic.ModelHandling;
using StarAPI.Logic.Utils;
using StarAPI.Models;

namespace StarAPI.Logic.Mappers
{
    public class CardMapper
    {
        private StarDeckContext _context;
        private RaceHandling _raceHandling;
        private CardTypeHandling _cardTypeHandling;
        private bool s_defaultActivationState = true;

        public CardMapper(StarDeckContext context)
        {
            _context = context;
            _raceHandling = new RaceHandling(_context);
            _cardTypeHandling = new CardTypeHandling(_context);
        }
        
        public OutputCard FillOutputCard(Card card)
        {
            OutputCard outputCard = new OutputCard
            {
                id = card.id,
                name = card.name,
                energy = card.energy,
                cost = card.cost,
                type = _cardTypeHandling.GetCardType(card.typeId),
                race = _raceHandling.GetRace(card.raceId),
                description = card.description,
                image = "Hola"
            };
            return outputCard;
        }

        public Card FillNewCard(InputCard newCard, string id)
        {
            Card card = new Card();
            card.id = id;
            card.name = newCard.name;
            card.energy = newCard.energy;
            card.cost = newCard.cost;
            card.typeId = newCard.typeId;
            card.raceId = newCard.raceId;
            card.activatedCard = s_defaultActivationState;
            card.description = newCard.description;
            return card;
        }

    }
}