using Game.Enemy.Factory;
using Game.Enemy.Factory.Impl;
using Game.Level.Provider.Impl;
using Game.Level.View.Impl;
using Game.Services.Level;
using Game.Services.Spawner.Impl;
using Game.Services.WaveTimer.Impl;
using Services.ScreenPosition.Impl;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    public class GameServicesInstaller : MonoInstaller
    {
        [SerializeField] private LevelView levelView;

        public override void InstallBindings()
        {
            BindLevel();

            BindFactories();

            BindServices();
        }

        private void BindLevel()
        {
            Container.BindInterfacesTo<LevelViewProvider>().AsSingle().WithArguments(levelView);
        }

        private void BindFactories()
        {
            Container.BindInterfacesTo<EnemyFactory>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<ScreenPositionService>().AsSingle();
            Container.BindInterfacesTo<SpawnService>().AsSingle();
            Container.BindInterfacesTo<LevelService>().AsSingle();
            Container.BindInterfacesTo<WaveTimerService>().AsSingle();
        }
    }
}