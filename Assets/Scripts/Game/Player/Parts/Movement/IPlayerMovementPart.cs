using System;
using Game.Object.Part;
using UniRx;

namespace Game.Player.Parts.Movement
{
    public interface IPlayerMovementPart : IObjectPart
    {
        IObservable<Unit> DashStarted { get; }

        void Enable();
        void Disable();
    }
}