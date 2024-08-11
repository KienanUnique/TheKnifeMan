using UniRx;

namespace Game.Services.Pause
{
    public interface IPauseService
    {
        IReactiveProperty<bool> IsPaused { get; }
        void DisablePause();
        void EnablePause();
    }
}