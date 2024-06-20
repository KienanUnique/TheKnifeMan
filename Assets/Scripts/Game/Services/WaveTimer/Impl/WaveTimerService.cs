using System;
using Game.Utils.Spawner;
using UniRx;

namespace Game.Services.WaveTimer.Impl
{
    public class WaveTimerService : IDisposable, IWaveTimerService
    {
        private readonly ReactiveCommand<TimeSpan> _onTick = new();
        private readonly ReactiveCommand _onTimerEnd = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        public IObservable<TimeSpan> OnTick => _onTick;
        public IObservable<Unit> OnTimerEnd => _onTimerEnd;

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public void StartTimer(WaveData waveData)
        {
            var delay = waveData.DelayBeforeSpawnSeconds;

            _compositeDisposable.Add(
                Observable.Interval(TimeSpan.FromSeconds(1))
                    .Take(delay)
                    .Select(_ => TimeSpan.FromSeconds(delay - _))
                    .Subscribe(remainingTime => _onTick.Execute(remainingTime), () => { _onTimerEnd.Execute(); })
            );
        }
    }
}