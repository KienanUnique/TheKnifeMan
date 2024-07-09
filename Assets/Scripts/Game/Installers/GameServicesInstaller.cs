using System;
using Game.Enemy.Factory.Impl;
using Game.Level.Provider.Impl;
using Game.Projectile.Factory.Impl;
using Game.Services.Score.Impl;
using Game.Services.SpawnEffects.SpawnEffects.Impl;
using Game.Services.Spawner.Impl;
using Game.Services.VFX.Impl;
using Game.Services.WaveTimer.Impl;
using Game.Utils;
using Services.ScreenPosition.Impl;
using Services.Sound.Impl;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [RequireComponent(typeof(LevelViewLink))]
    public class GameServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLevel();

            BindFactories();

            BindServices();
        }

        private void BindLevel()
        {
            var levelViewLink = GetComponent<LevelViewLink>();
            var levelView = levelViewLink.LevelView;
            Container.BindInterfacesTo<LevelViewProvider>().AsSingle().WithArguments(levelView);
        }

        private void BindFactories()
        {
            Container.BindInterfacesTo<EnemyFactory>().AsSingle();
            Container.BindInterfacesTo<ProjectilesFactory>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<ScreenPositionService>().AsSingle();
            Container.BindInterfacesTo<SpawnService>().AsSingle();
            Container.BindInterfacesTo<WaveTimerService>().AsSingle();
            Container.BindInterfacesTo<ScoreService>().AsSingle();
            Container.BindInterfacesTo<GameSoundFxService>().AsSingle();
            Container.BindInterfacesTo<VfxService>().AsSingle();
            Container.BindInterfacesTo<EnemySpawnEffectsService>().AsSingle();
            Container.Bind(typeof(IInitializable), typeof(IDisposable)).To<GameStateMachine.Impl.GameStateMachine>().AsSingle();
        }
    }
}