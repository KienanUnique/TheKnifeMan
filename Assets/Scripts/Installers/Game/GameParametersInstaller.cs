using Db.EnemiesParametersProvider;
using Db.EnemiesParametersProvider.Impl;
using Db.EnemyFactory;
using Db.Player;
using Db.Player.Impl;
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
        [SerializeField] private EnemiesParametersProvider enemiesParametersProvider;
        [SerializeField] private EnemyFactoryParameters enemyFactoryParameters;
        
        public override void InstallBindings()
        {
            Container.Bind<IPlayerParameters>().FromInstance(playerParameters).AsSingle();
            Container.Bind<IEnemiesParametersProvider>().FromInstance(enemiesParametersProvider).AsSingle();
            Container.Bind<IEnemyFactoryParameters>().FromInstance(enemyFactoryParameters).AsSingle();
        }
    }
}