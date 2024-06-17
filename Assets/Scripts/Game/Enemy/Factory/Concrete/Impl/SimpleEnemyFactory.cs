using Db.EnemyFactory;
using Game.Enemy.Controller.Impl;
using Game.Enemy.PartsFactory.Impl;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Factory.Concrete.Impl
{
    public class SimpleEnemyFactory : AConcreteEnemyFactory
    {
        public SimpleEnemyFactory(
            DiContainer diContainer,
            Transform rootTransform,
            IEnemyFactoryParameters parameters
        ) : base(diContainer, rootTransform, parameters)
        {
        }

        protected override string PrefabNameInResources => "P_Enemy_Simple";

        protected override void InstallBindings(DiContainer container)
        {
            container.BindInterfacesTo<SimpleEnemyPartsFactory>().AsSingle().WhenInjectedInto<SimpleEnemyController>();
        }
    }
}