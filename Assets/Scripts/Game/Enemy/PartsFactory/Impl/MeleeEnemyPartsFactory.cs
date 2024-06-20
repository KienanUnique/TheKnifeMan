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
    public class MeleeEnemyPartsFactory<TParameters> : AEnemyPartsFactory
        where TParameters : IMeleeEnemyParameters
    {
        private readonly TParameters _parameters;
        
        protected MeleeEnemyPartsFactory(
            DiContainer mainContainer,
            IEnemiesParametersProvider enemiesParametersProvider,
            TParameters parameters
        ) : base(mainContainer, enemiesParametersProvider)
        {
            _parameters = parameters;
        }

        protected override void HandleCreateParts(DiContainer container, object[] extraArgs,
            IEnemiesParametersProvider enemiesParametersProvider)
        {
            container.BindInterfacesTo<DefaultEnemyCharacterPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<DefaultEnemyLookDirectionPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<AnimatorStatusCheckerPart>().AsSingle().WithArguments(extraArgs);
            
            container.BindInterfacesTo<TParameters>().FromInstance(_parameters).AsSingle();

            container.BindInterfacesTo<MeleeAttackEnemyVisualPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<EnemyMeleeAttacker>().AsSingle().WithArguments(extraArgs);
        }
    }
}