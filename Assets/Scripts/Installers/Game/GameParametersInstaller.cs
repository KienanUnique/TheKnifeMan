using Db.EnemiesParametersProvider;
using Db.EnemiesParametersProvider.Impl;
using Db.EnemyFactory;
using Db.EnemyFactory.Impl;
using Db.LayerMasks;
using Db.LayerMasks.Impl;
using Db.Player;
using Db.Player.Impl;
using Db.Spawners;
using Db.Spawners.Impl;
using Db.Waves;
using Db.Waves.Impl;
using UnityEngine;
using Utils;
using Zenject;

namespace Installers.Game
{
    [CreateAssetMenu(menuName = MenuPathBase.Installers + nameof(GameParametersInstaller),
        fileName = nameof(GameParametersInstaller))]
    public class GameParametersInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PlayerParameters playerParameters;

        [Header("Enemies")] [SerializeField] private EnemiesParametersProvider enemiesParametersProvider;
        [SerializeField] private EnemyFactoryParameters enemyFactoryParameters;

        [Header("Spawn")] [SerializeField] private WavesParameters wavesParameters;
        [SerializeField] private SpawnersParameters spawnersParameters;

        [Header("Other")] [SerializeField] private LayerMasksParameters layerMasksParameters;

        public override void InstallBindings()
        {
            Container.Bind<IPlayerParameters>().FromInstance(playerParameters).AsSingle();

            Container.Bind<IEnemiesParametersProvider>().FromInstance(enemiesParametersProvider).AsSingle();
            Container.Bind<IEnemyFactoryParameters>().FromInstance(enemyFactoryParameters).AsSingle();

            Container.Bind<IWavesParameters>().FromInstance(wavesParameters).AsSingle();
            Container.Bind<ISpawnersParameters>().FromInstance(spawnersParameters).AsSingle();

            Container.Bind<ILayerMasksParameters>().FromInstance(layerMasksParameters).AsSingle();
        }
    }
}