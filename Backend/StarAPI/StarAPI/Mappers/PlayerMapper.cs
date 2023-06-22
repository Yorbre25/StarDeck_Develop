using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Models;
using Contracts;
using StarAPI.Constants;

namespace StarAPI.Logic.Mappers
{
    public class PlayerMapper
    {
        private IRepositoryWrapper _context;
        private CountryHandling _countryHandling;
        private ImageHandling _imageHandling;
        private Encrypt _encrypt;

        public PlayerMapper(IRepositoryWrapper context)
        {
            this._context = context;
            this._countryHandling = new CountryHandling(_context);
            this._imageHandling = new ImageHandling(_context);
            this._encrypt = new Encrypt();
        }
        
        public Player FillNewPlayer(InputPlayer newPlayer, string id)
        {
            Player player = new Player();
            player.id = id;
            player.email = newPlayer.email;
            player.firstName = newPlayer.firstName;
            player.lastName = newPlayer.lastName;
            player.username = newPlayer.username;
            player.pHash = _encrypt.Sha256(newPlayer.password);
            player.xp = Const.DefaultXP;
            player.ranking = Const.DefaultRanking;
            player.inGame = Const.DefaultInGameState;
            player.activatedAccount = Const.DefaultActivationState;
            player.countryId = newPlayer.countryId;
            player.coins = Const.DefaultCoins;
            player.avatarId = _imageHandling.GetImageId(newPlayer.avatar);
            return player;
        }

        public OutputPlayer FillOutputPlayer(Player player)
        {
            OutputPlayer outputCard = new OutputPlayer
            {
                id = player.id,
                email = player.email,
                firstName = player.firstName,
                lastName = player.lastName,
                username = player.username,
                pHash = player.pHash,
                xp = player.xp,
                ranking = player.ranking,
                country = _countryHandling.GetCountry(player.countryId),
                coins = player.coins,
                avatar = _imageHandling.GetImage(player.avatarId)
            };
            return outputCard;
        }

        public List<OutputPlayer> FillOutputPlayer(List<Player> players)
        {
            List<OutputPlayer> outputPlayers = new List<OutputPlayer>();
            foreach (Player player in players)
            {
                outputPlayers.Add(FillOutputPlayer(player));
            }
            return outputPlayers;
        }
    }
}