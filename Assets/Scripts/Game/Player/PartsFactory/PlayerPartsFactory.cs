using Game.Character.Parts.AnimatorStatus.Impl;
using Game.Object.PartsFactory;
using Game.Player.Parts.Attack.Impl;
using Game.Player.Parts.Character;
using Game.Player.Parts.LookDirection.Impl;
using Game.Player.Parts.Movement;
using Game.Player.Parts.Visual;
using Zenject;

namespace Game.Player.PartsFactory
{
    public class PlayerPartsFactory : APartsFactory
    {
        public PlayerPartsFactory(DiContainer mainContainer) : base(mainContainer)
        {
        }

        protected override void HandleCreateParts(DiContainer container, object[] extraArgs)
        {
            container.BindInterfacesTo<PlayerMovementPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<PlayerVisualPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<PlayerCharacterPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<PlayerAttackPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<PlayerLookDirectionPart>().AsSingle().WithArguments(extraArgs);
            container.BindInterfacesTo<AnimatorStatusCheckerPart>().AsSingle().WithArguments(extraArgs);
        }
    }
}