using Db.EnemiesParametersProvider;
using Db.EnemiesParametersProvider.Parameters.Impl;
using Game.Enemy.Parts.Character;
using Game.Enemy.Parts.Visual;
using Zenject;

namespace Game.Enemy.PartsFactory.Impl
{
    public class SimpleEnemyPartsFactory : AEnemyPartsFactory
    {
        public SimpleEnemyPartsFactory(
            DiContainer mainContainer,
            IEnemiesParametersProvider enemiesParametersProvider
        ) : base(mainContainer, enemiesParametersProvider)
        {
        }

        protected override void HandleCreateParts(DiContainer container, object[] extraArgs,
            IEnemiesParametersProvider enemiesParametersProvider)
        {
            container.BindInterfacesTo<DefaultEnemyCharacterPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<DefaultEnemyVisualPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<ISimpleEnemyParameters>()
                .FromInstance(enemiesParametersProvider.SimpleEnemyParameters).AsSingle();
        }
    }
}