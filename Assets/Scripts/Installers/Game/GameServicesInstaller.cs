using System;
using Game.Enemy.Factory;
using Game.Utils;
using Services.ScreenPosition.Impl;
using UniRx;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    public class GameServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ScreenPositionService>().AsSingle();
            Container.BindInterfacesTo<EnemyFactory>().AsSingle();
            // TODO: remove test logic
            Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ =>
            {
                var factory = Container.Resolve<IEnemyFactory>();
                factory.Create(EEnemyType.Simple, Vector3.zero);
            });
        }
    }
}