using Db.EnemiesParametersProvider;
using Db.EnemiesParametersProvider.Parameters.Impl;
using Game.Character.Parts.AnimatorStatus.Impl;
using Game.Enemy.Parts.Attacker.Impl;
using Game.Enemy.Parts.Character.Impl;
using Game.Enemy.Parts.LookDirection.Impl;
using Game.Enemy.Parts.Visual.Impl;
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
            container.BindInterfacesTo<DefaultEnemyLookDirectionPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<AnimatorStatusCheckerPart>().AsSingle().WithArguments(extraArgs);
            
            container.BindInterfacesTo<ISimpleEnemyParameters>()
                .FromInstance(enemiesParametersProvider.SimpleEnemyParameters).AsSingle();
            
            container.BindInterfacesTo<MeleeEnemyVisualPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<EnemyMeleeAttacker>().AsSingle().WithArguments(extraArgs);
        }
    }
}