using Db.EnemiesParametersProvider;
using Db.EnemiesParametersProvider.Parameters;
using Db.EnemiesParametersProvider.Parameters.Impl;
using Game.Character.Parts.AnimatorStatus.Impl;
using Game.Enemy.Parts.Attacker.Impl;
using Game.Enemy.Parts.Character.Impl;
using Game.Enemy.Parts.LookDirection.Impl;
using Game.Enemy.Parts.Visual.Impl;
using Zenject;

namespace Game.Enemy.PartsFactory.Impl
{
    public abstract class AMeleeEnemyPartsFactory<TParameters> : AEnemyPartsFactory
        where TParameters : IMeleeEnemyParameters
    {
        protected AMeleeEnemyPartsFactory(
            DiContainer mainContainer,
            IEnemiesParametersProvider enemiesParametersProvider
        ) : base(mainContainer, enemiesParametersProvider)
        {
        }

        protected abstract TParameters GetParameters(IEnemiesParametersProvider enemiesParametersProvider);

        protected override void HandleCreateParts(DiContainer container, object[] extraArgs,
            IEnemiesParametersProvider enemiesParametersProvider)
        {
            container.BindInterfacesTo<DefaultEnemyCharacterPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<DefaultEnemyLookDirectionPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<AnimatorStatusCheckerPart>().AsSingle().WithArguments(extraArgs);

            var parameters = GetParameters(enemiesParametersProvider);
            container.BindInterfacesTo<TParameters>().FromInstance(parameters).AsSingle();

            container.BindInterfacesTo<MeleeAttackEnemyVisualPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<EnemyMeleeAttacker>().AsSingle().WithArguments(extraArgs);
        }
    }
}