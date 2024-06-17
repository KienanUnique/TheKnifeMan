using Game.Utils;
using UnityEngine;

namespace Game.Enemy.Factory
{
    public interface IEnemyFactory
    {
        void Create(EEnemyType enemyType, Vector3 position);
    }
}