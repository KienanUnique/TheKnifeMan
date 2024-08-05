using Game.Character.Parts.AnimatorStatus.Impl;
using Game.Enemy.Parts.Attacker.Impl;
using Game.Enemy.Parts.Character.Impl;
using Game.Enemy.Parts.LookDirection.Impl;
using Game.Enemy.Parts.Visual.Impl;
using Zenject;

namespace Game.Enemy.PartsFactory.Impl
{
    public class UniversalEnemyPartsFactory : AEnemyPartsFactory
    {
        public UniversalEnemyPartsFactory(DiContainer mainContainer) : base(mainContainer)
        {
        }

        protected override void HandleCreateParts(DiContainer container, object[] extraArgs)
        {
            container.BindInterfacesTo<DefaultEnemyCharacterPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<DefaultEnemyLookDirectionPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<AnimatorStatusCheckerPart>().AsSingle().WithArguments(extraArgs);
            
            container.BindInterfacesTo<EnemyMeleeAttacker>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<ProjectileEnemyAttackDirectionPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<EnemyProjectileAttacker>().AsSingle().WithArguments(extraArgs);
            
            container.BindInterfacesTo<UniversalAttackEnemyVisualPart>().AsSingle().WithArguments(extraArgs);
        }
    }
}