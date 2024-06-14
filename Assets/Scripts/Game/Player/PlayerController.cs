using System.Collections.Generic;
using Game.Object;
using Game.Object.Part;
using Game.Object.PartsFactory;
using Game.Player.Parts;
using Game.Player.Parts.Movement;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerController : AObjectController<PlayerView>
    {
        [Inject] private IPartsFactory _partsFactory;

        private IPlayerMovementPart _movement;

        protected override List<IObjectPart> CreateParts(PlayerView view)
        {
            var allParts = new List<IObjectPart>();

            _partsFactory.CreateParts(new object[] {View});

            _movement = _partsFactory.Resolve<IPlayerMovementPart>();
            allParts.Add(_movement);

            return allParts;
        }

        protected override void HandleInitialize()
        {
            Debug.Log($"HandleInitialize");
            _movement.Enable();
        }
    }
}