using Db.EnemiesParameters.TypeData;
using UnityEngine;

namespace Game.Enemy.Factory
{
    public interface IEnemyFactory
    {
        void Create(IEnemyType type, Vector3 position);
    }
}