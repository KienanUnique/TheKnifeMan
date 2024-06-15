using Game.Object;
using Game.Player.Parts.Movement;
using Game.Player.Parts.Visual;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : AObjectController<PlayerData>, IPlayerInformation
    {
        [SerializeField] private PlayerData data;
        
        private IPlayerMovementPart _movement;
        private IPlayerVisualPart _visualPart;

        public Transform Transform => transform;
        protected override PlayerData Data => data;

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