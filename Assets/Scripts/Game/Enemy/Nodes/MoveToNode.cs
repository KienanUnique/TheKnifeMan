using System;
using Game.Enemy.Context;
using Game.Player;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Nodes
{
    [Serializable]
    public class MoveToNode : ActionNodeWithContext<IEnemyContextBase>
    {
        [Inject] private IPlayerInformation _information;

        private Transform _playerTransform;

        protected override void HandleInitialize()
        {
            _playerTransform = _information.Transform;
        }

        protected override ENodeState OnUpdate()
        {
            ConcreteContext.DefaultActionsExecutor.SetDestination(_playerTransform.position);
            return ENodeState.Success;
        }
    }
}