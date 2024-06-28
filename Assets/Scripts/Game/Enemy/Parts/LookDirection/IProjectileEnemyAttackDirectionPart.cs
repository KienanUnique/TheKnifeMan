using UniRx;
using UnityEngine;

namespace Game.Enemy.Parts.LookDirection
{
    public interface IProjectileEnemyAttackDirectionPart : IEnemyPoolPart
    {
        IReactiveProperty<Vector2> AttackDirection { get; }
    }
}