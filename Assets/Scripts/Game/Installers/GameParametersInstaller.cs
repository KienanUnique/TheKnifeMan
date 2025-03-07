﻿using Db.EnemiesParameters.EnemiesTypeProvider;
using Db.EnemiesParameters.EnemiesTypeProvider.Impl;
using Db.EnemyFactory;
using Db.EnemyFactory.Impl;
using Db.EnemySpawnFx;
using Db.EnemySpawnFx.Impl;
using Db.LayerMasks;
using Db.LayerMasks.Impl;
using Db.Player;
using Db.Player.Impl;
using Db.Projectiles;
using Db.Projectiles.Impl;
using Db.Score;
using Db.Score.Impl;
using Db.Spawners;
using Db.Spawners.Impl;
using Db.Vfx;
using Db.Vfx.Impl;
using Game.Utils.Waves;
using Game.Utils.Waves.Impl;
using UnityEngine;
using Utils;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(menuName = MenuPathBase.Installers + nameof(GameParametersInstaller),
        fileName = nameof(GameParametersInstaller))]
    public class GameParametersInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PlayerParameters playerParameters;

        [Header("Enemies")] [SerializeField] private EnemiesTypeDataProvider enemiesTypeDataProvider;
        [SerializeField] private EnemyFactoryParameters enemyFactoryParameters;
        [SerializeField] private ProjectilesParameters projectilesParameters;

        [Header("Spawn")] [SerializeField] private WavesParameters wavesParameters;
        [SerializeField] private SpawnersParameters spawnersParameters;

        [Header("Other")] 
        [SerializeField] private LayerMasksParameters layerMasksParameters;
        [SerializeField] private VfxBase vfxBase;
        [SerializeField] private EnemySpawnFxBase enemySpawnFxBase;
        [SerializeField] private ScoreParameters scoreParameters;

        public override void InstallBindings()
        {
            Container.Bind<IPlayerParameters>().FromInstance(playerParameters).AsSingle();

            Container.Bind<IEnemiesTypeDataProvider>().FromInstance(enemiesTypeDataProvider).AsSingle();
            Container.Bind<IEnemyFactoryParameters>().FromInstance(enemyFactoryParameters).AsSingle();
            Container.Bind<IProjectilesParameters>().FromInstance(projectilesParameters).AsSingle();

            Container.Bind<IWavesParameters>().FromInstance(wavesParameters).AsSingle();
            Container.Bind<ISpawnersParameters>().FromInstance(spawnersParameters).AsSingle();

            Container.Bind<ILayerMasksParameters>().FromInstance(layerMasksParameters).AsSingle();
            Container.Bind<IVfxBase>().FromInstance(vfxBase).AsSingle();
            Container.Bind<IEnemySpawnFxBase>().FromInstance(enemySpawnFxBase).AsSingle();
            Container.Bind<IScoreParameters>().FromInstance(scoreParameters).AsSingle();
        }
    }
}