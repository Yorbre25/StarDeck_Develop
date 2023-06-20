using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Models;
using Contracts;

namespace StarAPI.Logic.Mappers
{
    public class DeckMapper
    {
        private IRepositoryWrapper _repository;
        private RaceHandling _raceHandling;
        private CardTypeHandling _cardTypeHandling;
        private bool s_defaultActivationState = true;

        public DeckMapper(IRepositoryWrapper repository)
        {
            _repository = repository;
            _raceHandling = new RaceHandling(_repository);
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