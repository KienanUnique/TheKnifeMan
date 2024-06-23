using UnityEngine;

namespace Game.Enemy.Data
{
    public interface IProjectileEnemyData : IEnemyData
    {
        Transform ProjectilesSpawnPoint { get; }
    }
}