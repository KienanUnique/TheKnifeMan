using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Db.EnemySpawnFx.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(EnemySpawnFxBase), fileName = nameof(EnemySpawnFxBase))]
    public class EnemySpawnFxBase : ScriptableObject, IEnemySpawnFxBase
    {
        [SerializeField] private List<Animator> allEffects;

        public IReadOnlyList<Animator> AllEffects => allEffects;
    }
}