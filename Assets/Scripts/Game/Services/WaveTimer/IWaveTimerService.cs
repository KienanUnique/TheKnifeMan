﻿using System;
using Game.Utils.Spawner;
using UniRx;

namespace Game.Services.WaveTimer
{
    public interface IWaveTimerService
    {
        IReactiveProperty<TimeSpan> RemainingTime { get; }
        IObservable<Unit> OnTimerEnd { get; }
        bool IsTimerRunning { get; }

        void StartTimer(WaveData waveData);
    }
}