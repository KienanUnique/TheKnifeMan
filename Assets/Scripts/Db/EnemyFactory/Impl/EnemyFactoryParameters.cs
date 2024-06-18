using UnityEngine;
using Utils;

namespace Db.EnemyFactory.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(EnemyFactoryParameters),
        fileName = nameof(EnemyFactoryParameters))]
    public class EnemyFactoryParameters : ScriptableObject, IEnemyFactoryParameters
    {
        [SerializeField] private float afterDeathDelaySeconds;

        public float AfterDeathDelaySeconds => afterDeathDelaySeconds;
    }
}