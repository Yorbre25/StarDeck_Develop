using Contracts;
using StarAPI.Context;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private StarDeckContext _repoContext;
        private ICardTypeRepository _cardType;
        private IGameRepository _game;
        private IGameDeckRepository _gameDeck;
        private IGamePlayerRepository _gamePlayer;
        private IGamePlanetRepository _gamePlanet;
        private IDeckRepository _deck;
        private IDeckCardRepository _deckCard;
        private ICardRepository _card;
        private IPlanetRepository _planet;
        private IImageRepository _image;
        private IRaceRepository _race;
        private IPlanetTypeRepository _planetType;
        private IPlayerRepository _player;
        private IPlayerCardRepository _playerCard;
        private IGameTableRepository _gameTable;
        private ICountryRepository _country;
        private IHandRepository _hand;

        private IMatchPlayerRepository _matchPlayer;

        public ICardTypeRepository CardType {
            get {
                if(_cardType == null)
                {
                    _cardType = new CardTypeRepository(_repoContext);
                }
                return _cardType;
            }
        }

        public IGameRepository Game {
            get {
                if(_game == null)
                {
                    _game = new GameRepository(_repoContext);
                }
                return _game;
            }
        }

        public IGameDeckRepository GameDeck {
            get {
                if(_gameDeck == null)
                {
                    _gameDeck = new GameDeckRepository(_repoContext);
                }
                return _gameDeck;
            }
        }

        public IGamePlayerRepository GamePlayer{
            get {
                if(_gamePlayer == null)
                {
                    _gamePlayer = new GamePlayerRepository(_repoContext);
                }
                return _gamePlayer;
            }
        }
        public IGamePlanetRepository GamePlanet{
            get {
                if(_gamePlanet == null)
                {
                    _gamePlanet = new GamePlanetRepository(_repoContext);
                }
                return _gamePlanet;
            }
        }
        public IDeckRepository Deck{
            get {
                if(_deck == null)
                {
                    _deck = new DeckRepository(_repoContext);
                }
                return _deck;
            }
        }
        public IDeckCardRepository DeckCard{
            get {
                if(_deckCard == null)
                {
                    _deckCard = new DeckCardRepository(_repoContext);
                }
                return _deckCard;
            }
        }
        public ICardRepository Card{
            get {
                if(_card == null)
                {
                    _card = new CardRepository(_repoContext);
                }
                return _card;
            }
        }
        public IPlanetRepository Planet{
            get {
                if(_planet == null)
                {
                    _planet = new PlanetRepository(_repoContext);
                }
                return _planet;
            }
        }
        public IImageRepository Image{
            get {
                if(_image == null)
                {
                    _image = new ImageRepository(_repoContext);
                }
                return _image;
            }
        }

        public IRaceRepository Race{
            get{
                if(_race == null)
                {
                    _race = new RaceRepository(_repoContext);
                }
                return _race;
            }
        }

        public IPlanetTypeRepository PlanetType{
            get{
                if(_planetType == null)
                {
                    _planetType = new PlanetTypeRepository(_repoContext);
                }
                return _planetType;
            }
        }

        public IPlayerRepository Player{
            get{
                if(_player == null)
                {
                    _player = new PlayerRepository(_repoContext);
                }
                return _player;
            }
        }

        public IPlayerCardRepository PlayerCard{
            get{
                if(_playerCard == null)
                {
                    _playerCard = new PlayerCardRepository(_repoContext);
                }
                return _playerCard;
            }
        }

        public IGameTableRepository GameTable{
            get{
                if(_gameTable == null)
                {
                    _gameTable = new GameTableRepository(_repoContext);
                }
                return _gameTable;
            }
        }

        public ICountryRepository Country{
            get{
                if(_country == null)
                {
                    _country = new CountryRepository(_repoContext);
                }
                return _country;
            }
        }

        public IHandRepository Hand{
            get{
                if(_hand == null)
                {
                    _hand = new HandRepository(_repoContext);
                }
                return _hand;
            }
        }

        public IMatchPlayerRepository MatchPlayer{
            get{
                if(_matchPlayer == null)
                {
                    _matchPlayer = new MatchPlayerRepository(_repoContext);
                }
                return _matchPlayer;
            }
        }

        public RepositoryWrapper(StarDeckContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}