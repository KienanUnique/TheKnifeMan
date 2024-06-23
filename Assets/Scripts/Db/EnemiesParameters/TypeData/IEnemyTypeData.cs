using Db.EnemiesParameters.Parameters;
using Game.Utils;
using UnityEngine;

namespace Db.EnemiesParameters.TypeData
{
    public interface IEnemyTypeData : IEnemyType
    {
        Object Prefab { get; }
        IEnemyParametersBase Parameters { get; }
        EEnemyType Type { get; }
    }
}