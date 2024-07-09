using System.Collections.Generic;
using UnityEngine;

namespace Db.EnemySpawnFx
{
    public interface IEnemySpawnFxBase
    {
        IReadOnlyList<Animator> AllEffects { get; }
    }
}