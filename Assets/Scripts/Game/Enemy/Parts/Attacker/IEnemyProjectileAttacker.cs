using UnityEngine;

namespace Game.Enemy.Parts.Attacker
{
    public interface IEnemyProjectileAttacker : IEnemyPoolPart
    {
        bool IsCanShoot { get; }
        void AttackWithProjectile(Vector2 attackDirection);
    }
}