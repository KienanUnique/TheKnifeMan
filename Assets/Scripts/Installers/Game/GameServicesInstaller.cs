using System;
using Game.Enemy.Factory;
using Game.Level.Provider.Impl;
using Game.Level.View.Impl;
using Game.Services.Level;
using Game.Services.Spawner.Impl;
using Game.Services.WaveTimer.Impl;
using Game.Utils;
using Services.ScreenPosition.Impl;
using UniRx;
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

            Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ =>
            {
                var factory = Container.Resolve<IEnemyFactory>();
                factory.Create(EEnemyType.Simple, Vector3.zero);
            });
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