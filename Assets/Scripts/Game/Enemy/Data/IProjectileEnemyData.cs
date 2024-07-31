using UnityEngine;

namespace Game.Enemy.Data
{
    public interface IProjectileEnemyData : IEnemyData
    {
        Transform ProjectilesSpawnPointLeft { get; }
        Transform ProjectilesSpawnPointRight { get; }
    }
}