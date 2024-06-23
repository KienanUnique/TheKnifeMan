using System.Collections.Generic;
using Game.Projectile.TypeData;

namespace Db.Projectiles
{
    public interface IProjectilesParameters
    {
        IReadOnlyList<IProjectileTypeData> ProjectilesTypes { get; }
    }
}