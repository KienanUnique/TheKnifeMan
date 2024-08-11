using System;
using Game.Utils.Spawner;
using UniRx;

namespace Game.Services.WaveTimer.Impl
{
    public class WaveTimerService : IDisposable, IWaveTimerService
    {
        private readonly ReactiveCommand _onTimerEnd = new();
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly ReactiveProperty<TimeSpan> _remainingTime = new(TimeSpan.Zero);
        
        private bool _isTimerRunning;
        
        public IReactiveProperty<TimeSpan> RemainingTime => _remainingTime;

        public IObservable<Unit> OnTimerEnd => _onTimerEnd;

        public bool IsTimerRunning => _isTimerRunning;

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public void StartTimer(WaveData waveData)
        {
            _isTimerRunning = true;
            
            var delay = waveData.DelayBeforeSpawnSeconds;
            
            _compositeDisposable.Add(
                Observable.Interval(TimeSpan.FromSeconds(1f))
                    .Take(delay + 1)
                    .Select(_ => TimeSpan.FromSeconds(delay - _))
                    .Subscribe(remainingTime => _remainingTime.Value = remainingTime.Add(TimeSpan.FromSeconds(-1)), () =>
                    {
                        _remainingTime.Value = TimeSpan.Zero;
                        _isTimerRunning = false;
                        _onTimerEnd.Execute();
                    })
            );
        }
    }
}