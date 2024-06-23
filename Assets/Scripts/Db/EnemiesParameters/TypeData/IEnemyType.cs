using System;

namespace Db.EnemiesParameters.TypeData
{
    public interface IEnemyType : IEquatable<IEnemyTypeData>
    {
        int InstanceID { get; }
    }
}