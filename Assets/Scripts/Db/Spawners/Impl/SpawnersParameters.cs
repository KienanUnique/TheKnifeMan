using UnityEngine;
using Utils;

namespace Db.Spawners.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(SpawnersParameters),
        fileName = nameof(SpawnersParameters))]
    public class SpawnersParameters : ScriptableObject, ISpawnersParameters
    {
        [SerializeField] [Min(0f)] private float spawnPointSpawnDelaySeconds;

        public float SpawnPointSpawnDelaySeconds => spawnPointSpawnDelaySeconds;
    }
}