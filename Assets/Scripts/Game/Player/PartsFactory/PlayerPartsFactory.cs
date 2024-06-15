using Game.Object.PartsFactory;
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
        }
    }
}