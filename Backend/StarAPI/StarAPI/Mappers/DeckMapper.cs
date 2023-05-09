using StarAPI.Context;
using StarAPI.DTOs;
using StarAPI.Logic.ModelHandling;
using StarAPI.Logic.Utils;
using StarAPI.Models;

namespace StarAPI.Logic.Mappers
{
    public class DeckMapper
    {
        private StarDeckContext _context;
        private RaceHandling _raceHandling;
        private CardTypeHandling _cardTypeHandling;
        private bool s_defaultActivationState = true;

        public DeckMapper(StarDeckContext context)
        {
            _context = context;
            _raceHandling = new RaceHandling(_context);
            _cardTypeHandling = new CardTypeHandling(_context);
        }
        
        public Deck FillNewDeck(InputDeck inputDeck, string id)
        {
            Deck deck = new Deck();
            deck.id = id;
            deck.name = inputDeck.name;
            deck.playerId = inputDeck.playerId;
            return deck;
        }

        public OutputDeck FillOutputDeck(Deck deck)
        {
            OutputDeck outputDeck = new OutputDeck
            {
                id = deck.id,
                name = deck.name,
            };
            return outputDeck;
        }
    }
}