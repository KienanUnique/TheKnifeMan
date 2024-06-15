using Game.Object;
using Game.Player.Parts;
using Game.Player.Parts.Movement;
using Game.Player.Parts.Visual;

namespace Game.Player
{
    public class PlayerController : AObjectController<PlayerView>
    {
        private IPlayerMovementPart _movement;
        private IPlayerVisualPart _visualPart;

        protected override void ResolveParts()
        {
            _movement = Resolve<IPlayerMovementPart>();
            _visualPart = Resolve<IPlayerVisualPart>();
        }

        protected override void HandleInitialize()
        {
            _movement.Enable();
        }
    }
}