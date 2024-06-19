using System;
using Game.Enemy.Context;
using Game.Player;
using Game.Utils;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Nodes.States
{
    [Serializable]
    public class DistanceToPlayer : ActionNodeWithContext<IEnemyContextBase>
    {
        public NodeProperty<ECompareType> compareType = new();
        public NodeProperty<float> compareValue = new();

        [Inject] private IPlayerInformation _information;

        private Transform _playerTransform;
        private Transform _enemyTransform;

        protected override void HandleInitialize()
        {
            _playerTransform = _information.Transform;
            _enemyTransform = ConcreteContext.Transform;
        }

        protected override ENodeState OnUpdate()
        {
            var distance = (_enemyTransform.position - _playerTransform.position).sqrMagnitude;
            var needDistance = compareValue.Value * compareValue.Value;
            return compareType.Value switch
            {
                ECompareType.Less => needDistance < distance ? ENodeState.Success : ENodeState.Failure,
                ECompareType.More => needDistance > distance ? ENodeState.Success : ENodeState.Failure,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}