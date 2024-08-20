using System;
using System.Collections.Generic;
using System.Linq;
using Db.EnemiesParameters.TypeData;
using Db.Spawners;
using Game.Enemy.Factory;
using Game.Level.Provider;
using Game.SpawnPoint;
using Game.Utils;
using Game.Utils.Spawner;
using ModestTree;
using UniRx;
using Zenject;

namespace Game.Services.Spawner.Impl
{
    public class SpawnService : IInitializable, IDisposable, ISpawnService, IGameStateListener
    {
        private readonly ILevelViewProvider _levelViewProvider;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ISpawnersParameters _spawnersParameters;

        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly List<IEnemyType> _spawnOrder = new();
        private readonly List<IEnemySpawnPoint> _freeSpawnPoints = new();

        public SpawnService(
            ILevelViewProvider levelViewProvider,
            IEnemyFactory enemyFactory,
            ISpawnersParameters spawnersParameters
        )
        {
            _levelViewProvider = levelViewProvider;
            _enemyFactory = enemyFactory;
            _spawnersParameters = spawnersParameters;
        }

        public void Initialize()
        {
            var spawnPoints = _levelViewProvider.LevelView.EnemySpawnPoints;
            foreach (var spawnPoint in spawnPoints)
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
                _spawnOrder.AddRange(Enumerable.Repeat(enemyWaveSpawnData.Enemy, enemyWaveSpawnData.Count));

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

        public void ForceStopSpawning()
        {
            _spawnOrder.Clear();
        }

        private void OnSpawnPointBecomeFree(IEnemySpawnPoint enemySpawnPoint)
        {
            if (_freeSpawnPoints.Contains(enemySpawnPoint))
                return;

            if (_spawnOrder.IsEmpty())
            {
                _freeSpawnPoints.Add(enemySpawnPoint);
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

        public void OnGameEnd(bool isPlayerWin)
        {
            _compositeDisposable?.Dispose();
            _spawnOrder.Clear();
        }
    }
}