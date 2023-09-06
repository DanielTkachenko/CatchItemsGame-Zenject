namespace CatchItemsGame
{
    public interface IFactoryCharacter
    {
        PlayerView Create(PlayerModel playerModel, PlayerView playerView);
    }
}