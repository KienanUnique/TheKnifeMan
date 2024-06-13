using System;
using UniRx;
using UnityEngine;

namespace Services.Input
{
    public interface IInputService
    {
        Vector2 NeedDirection { get; }
        IReactiveProperty<bool> IsDashPressed { get; }
        IObservable<Unit> PausePressed { get; }
    }
}