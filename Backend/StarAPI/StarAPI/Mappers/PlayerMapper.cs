using StarAPI.Context;
using StarAPI.DTOs;
using StarAPI.Logic.ModelHandling;
using StarAPI.Logic.Utils;
using StarAPI.Models;

namespace StarAPI.Logic.Mappers
{
    public class PlayerMapper
    {
        private StarDeckContext _context;
        private CountryHandling _countryHandling;
        private ImageHandling _imageHandling;
        private Encrypt _encrypt;
        private bool s_defaultActivationState = true;
        private static int s_defaultXp = 0;
        private static int s_defaultRanking = 0;
        private static bool  s_defaultInGameState = false;
        private static int s_defaultCoins = 0;

        public PlayerMapper(StarDeckContext context)
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
            player.xp = s_defaultXp;
            player.ranking = s_defaultRanking;
            player.inGame = s_defaultInGameState;
            player.activatedAccount = s_defaultActivationState;
            player.countryId = newPlayer.countryId;
            player.coins = s_defaultCoins;
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