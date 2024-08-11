namespace Game.Utils
{
    public interface IGameStateListener
    {
        void OnGameEnd(bool isPlayerWin);
    }
}