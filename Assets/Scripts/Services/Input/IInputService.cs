using System;
using UniRx;
using UnityEngine;

namespace Services.Input
{
    public interface IInputService
    {
        Vector2 NeedDirection { get; }
        Vector2 MousePosition { get; }
        IReactiveProperty<bool> IsDashPressed { get; }
        IObservable<Unit> PausePressed { get; }
        IObservable<Unit> CloseWindowPressed { get; }
        IObservable<Unit> AttackPressed { get; }
        IObservable<Unit> AnyKeyPressed { get; }
        IObservable<Unit> RestartLevelPressed { get; }

        void SwitchToUiInput();
        void SwitchToGameInput();
        void SwitchToAnyKeyInput();
    }
}