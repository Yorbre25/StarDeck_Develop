using StarAPI.DTO.Discovery;
using StarAPI.Models;
using Contracts;
using StarAPI.DataHandling.Discovery;

namespace StarAPI.Logic.Mappers
{
    public class CardMapper
    {
        private IRepositoryWrapper _repository;
        private RaceHandling _raceHandling;
        private CardTypeHandling _cardTypeHandling;
        private ImageHandling _imageHandling;
        private bool s_defaultActivationState = true;


        public CardMapper(IRepositoryWrapper repository)
        {
            _repository = repository;
            _raceHandling = new RaceHandling(_repository);
            _cardTypeHandling = new CardTypeHandling(_repository);
            _imageHandling = new ImageHandling(_repository);
        }
        
        public OutputCard FillOutputCard(Card card)
        {
            OutputCard outputCard = new OutputCard
            {
                id = card.id,
                name = card.name,
                energy = card.energy,
                cost = card.cost,
                type = _cardTypeHandling.GetCardTypeName(card.typeId),
                race = _raceHandling.GetRace(card.raceId),
                description = card.description,
                image = _imageHandling.GetImage(card.imageId)
            };
            return outputCard;
        }

        public List<OutputCard> FillOutputCard(List<Card> cards)
        {
            List<OutputCard> outputCards = new List<OutputCard>();
            foreach (Card card in cards)
            {
                outputCards.Add(FillOutputCard(card));
            }
            return outputCards;
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
            card.imageId = _imageHandling.GetImageId(newCard.image);
            return card;
        }

    }
}