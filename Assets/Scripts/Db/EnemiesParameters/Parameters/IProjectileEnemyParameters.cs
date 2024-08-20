using Game.Projectile.Pattern;
using Game.Projectile.TypeData;

namespace Db.EnemiesParameters.Parameters
{
    public interface IProjectileEnemyParameters : IEnemyParametersBase
    {
        float ReloadDurationSeconds { get; }
        int CountOfAttacksInClip { get; }
        float DelayBetweenShootAttacks { get; }
        IProjectilesPattern Pattern { get; }
        IProjectileType Type { get; }
    }
}