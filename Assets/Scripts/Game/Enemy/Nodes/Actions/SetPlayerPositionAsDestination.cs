using System;
using Game.Player;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Nodes.Actions
{
    [Serializable]
    public class SetPlayerPositionAsDestination : AAiActionNode
    {
        [Inject] private IPlayerInformation _information;

        private Transform _playerTransform;

        protected override void Initialize()
        {
            _playerTransform = _information.Transform;
        }

        protected override ENodeState OnUpdate()
        {
            ConcreteContext.DefaultControllableEnemy.SetDestination(_playerTransform.position);
            return ENodeState.Success;
        }
    }
}