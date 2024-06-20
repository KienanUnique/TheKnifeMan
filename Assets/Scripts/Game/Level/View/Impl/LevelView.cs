using System.Collections.Generic;
using Alchemy.Inspector;
using Game.Player;
using Game.SpawnPoint;
using Game.SpawnPoint.Impl;
using UnityEngine;

namespace Game.Level.View.Impl
{
    public class LevelView : MonoBehaviour, ILevelView
    {
        [SerializeField] private PlayerSpawnPoint playerSpawnPoint;
        [SerializeField] private List<EnemySpawnPoint> enemySpawnPoints;

        public Vector3 PlayerSpawnPoint => playerSpawnPoint.transform.position;
        public List<IEnemySpawnPoint> EnemySpawnPoints => new(enemySpawnPoints);

        [Button]
        public void AutoFill()
        {
            playerSpawnPoint = FindObjectOfType<PlayerSpawnPoint>();

            enemySpawnPoints.Clear();
            enemySpawnPoints.AddRange(FindObjectsOfType<EnemySpawnPoint>());
        }
    }
}