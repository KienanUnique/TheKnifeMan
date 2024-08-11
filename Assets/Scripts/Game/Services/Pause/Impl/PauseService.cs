using UniRx;

namespace Game.Services.Pause.Impl
{
    public class PauseService : IPauseService
    {
        private readonly ReactiveProperty<bool> _isPaused = new();

        public IReactiveProperty<bool> IsPaused => _isPaused;

        public void DisablePause()
        {
            _isPaused.Value = false;
        }

        public void EnablePause()
        {
            _isPaused.Value = true;
        }
    }
}