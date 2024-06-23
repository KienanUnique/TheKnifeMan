using Game.Projectile.Pattern;

namespace Game.Enemy.ActionsExecutor
{
    public interface IProjectileAttackEnemy : IDefaultControllableEnemy
    {
        void AttackWithProjectile(IProjectilesPattern pattern);
    }
}