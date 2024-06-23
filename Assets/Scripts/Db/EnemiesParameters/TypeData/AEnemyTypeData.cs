using Db.EnemiesParameters.Parameters;
using Game.Utils;
using UnityEngine;

namespace Db.EnemiesParameters.TypeData
{
    public abstract class AEnemyTypeData : ScriptableObject, IEnemyTypeData
    {
        [SerializeField] private int startPoolCount = 4;
        public abstract Object Prefab { get; }
        public abstract IEnemyParametersBase Parameters { get; }
        public abstract EEnemyType Type { get; }
        public int StartPoolCount => startPoolCount;

        public int InstanceID => GetInstanceID();

        public bool Equals(IEnemyTypeData other)
        {
            return other != null && InstanceID.Equals(other.InstanceID);
        }
    }
}