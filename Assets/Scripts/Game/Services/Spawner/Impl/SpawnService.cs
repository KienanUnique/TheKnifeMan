using System;
using System.Collections.Generic;
using System.Linq;
using Db.Spawners;
using Game.Enemy.Factory;
using Game.SpawnPoint;
using Game.Utils;
using Game.Utils.Spawner;
using ModestTree;
using UniRx;
using Unity.VisualScripting;

namespace Game.Services.Spawner.Impl
{
    public class SpawnService : IInitializable, IDisposable, ISpawnService
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly List<EEnemyType> _spawnOrder = new();
        private readonly List<IEnemySpawnPoint> _freeSpawnPoints = new();

        private readonly List<IEnemySpawnPoint> _spawnPoints;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ISpawnersParameters _spawnersParameters;

        public SpawnService(
            List<IEnemySpawnPoint> spawnPoints,
            IEnemyFactory enemyFactory,
            ISpawnersParameters spawnersParameters
        )
        {
            _spawnPoints = spawnPoints;
            _enemyFactory = enemyFactory;
            _spawnersParameters = spawnersParameters;
        }

        public void Initialize()
        {
            foreach (var spawnPoint in _spawnPoints)
            {
                _freeSpawnPoints.Add(spawnPoint);
                spawnPoint.OnBecomeFree.Subscribe(OnSpawnPointBecomeFree).AddTo(_compositeDisposable);
            }
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public void SpawnWave(WaveData wave)
        {
            foreach (var enemyWaveSpawnData in wave.Enemies)
            {
                _spawnOrder.AddRange(Enumerable.Repeat(enemyWaveSpawnData.Enemy, enemyWaveSpawnData.Count));
            }

            var spawnDelay = _spawnersParameters.SpawnPointSpawnDelaySeconds;
            while (!_freeSpawnPoints.IsEmpty() && !_spawnOrder.IsEmpty())
            {
                var spawnPoint = _freeSpawnPoints[0];
                var enemy = _spawnOrder[0];
                
                _enemyFactory.Create(enemy, spawnPoint.Position);
                
                spawnPoint.SetBusy(spawnDelay);
                _freeSpawnPoints.RemoveAt(0);
                _spawnOrder.RemoveAt(0);
            }
        }

        private void OnSpawnPointBecomeFree(IEnemySpawnPoint enemySpawnPoint)
        {
            if(_spawnPoints.Contains(enemySpawnPoint))
                return;

            if (_spawnOrder.IsEmpty())
            {
                _spawnPoints.Add(enemySpawnPoint);
            }
            else
            {
                var enemy = _spawnOrder[0];

                _enemyFactory.Create(enemy, enemySpawnPoint.Position);
                
                var spawnDelay = _spawnersParameters.SpawnPointSpawnDelaySeconds;
                enemySpawnPoint.SetBusy(spawnDelay);

                _spawnOrder.RemoveAt(0);
            }
        }
    }
}