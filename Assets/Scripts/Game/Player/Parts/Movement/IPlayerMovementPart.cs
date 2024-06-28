using System;
using Game.Object.Part;
using UniRx;

namespace Game.Player.Parts.Movement
{
    public interface IPlayerMovementPart : IObjectPart
    {
        IObservable<Unit> DashStarted { get; }
        IObservable<Unit> DashEnded { get; }

        void Enable();
        void Disable();
    }
}