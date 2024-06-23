using Game.Projectile.Pattern;
using Game.Projectile.TypeData;
using UnityEngine;

namespace Game.Enemy.Parts.Attacker
{
    public interface IEnemyProjectileAttacker : IEnemyPoolPart
    {
        void AttackWithProjectile(IProjectilesPattern pattern, IProjectileType type, Vector2 attackDirection);
    }
}