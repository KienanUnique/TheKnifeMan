using System;
using Game.Utils.Spawner;
using UniRx;

namespace Game.Services.WaveTimer
{
    public interface IWaveTimerService
    {
        IObservable<TimeSpan> OnTick { get; }
        IObservable<Unit> OnTimerEnd { get; }

        void StartTimer(WaveData waveData);
    }
}