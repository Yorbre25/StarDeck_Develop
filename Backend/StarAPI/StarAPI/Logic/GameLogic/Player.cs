using StarAPI.DTOs;

namespace StarAPI.Logic.GameLogic;

public class GamePlayer
{
    // public Player(int maxHandSize)
    // {
    //     this.maxHandSize = maxHandSize;
    // }

    public OutputCard[] hand { get; set;}
    public OutputCard[] deck { get; set;}

    private int maxHandSize { get; set; }

    public OutputPlayer playerInfo { get; set; }

    int battlePoints { get; set; }
}