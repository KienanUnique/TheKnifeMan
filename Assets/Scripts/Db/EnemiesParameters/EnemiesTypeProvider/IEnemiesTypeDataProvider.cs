using System.Collections.Generic;
using Db.EnemiesParameters.TypeData;

namespace Db.EnemiesParameters.EnemiesTypeProvider
{
    public interface IEnemiesTypeDataProvider
    {
        IReadOnlyList<IEnemyTypeData> AllEnemiesTypes { get; }
    }
}