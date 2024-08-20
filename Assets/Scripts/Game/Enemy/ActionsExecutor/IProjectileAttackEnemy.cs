using Game.Projectile;
using Game.Projectile.Pattern;
using Game.Projectile.TypeData;

namespace Game.Enemy.ActionsExecutor
{
    public interface IProjectileAttackEnemy : IDefaultControllableEnemy, IProjectilesSender
    {
        bool IsCanShoot { get; }
        void AttackWithProjectile();
    }
}