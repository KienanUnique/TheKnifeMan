using Game.Object;
using Game.Player.Parts;
using Game.Player.Parts.Movement;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : AObjectController<PlayerView>
    {
        private IPlayerMovementPart _movement;

        protected override void ResolveParts()
        {
            _movement = Resolve<IPlayerMovementPart>();
        }

        protected override void HandleInitialize()
        {
            Debug.Log($"HandleInitialize");
            _movement.Enable();
        }
    }
}