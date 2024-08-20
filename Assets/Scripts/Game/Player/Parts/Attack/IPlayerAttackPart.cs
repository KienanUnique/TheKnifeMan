using System;
using Game.Object.Part;
using Game.Utils.Directions;
using UniRx;

namespace Game.Player.Parts.Attack
{
    public interface IPlayerAttackPart : IObjectPart
    {
        IObservable<Unit> Attack { get; }
        IObservable<Unit> EnemyDamaged { get; }

        void DamageTargets(EDirection2D attackDirection);
    }
}