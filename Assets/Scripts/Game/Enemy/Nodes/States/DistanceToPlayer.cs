using System;
using Game.Enemy.Context;
using Game.Player;
using Game.Utils;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Nodes.States
{
    [Serializable]
    public class DistanceToPlayer : AAiActionNode
    {
        public NodeProperty<ECompareType> compareType = new();
        public NodeProperty<float> compareValue = new();

        [Inject] private IPlayerInformation _information;

        private Transform _playerTransform;
        private Transform _enemyTransform;

        protected override void Initialize()
        {
            _playerTransform = _information.Transform;
            _enemyTransform = ConcreteContext.Transform;
        }

        protected override ENodeState OnUpdate()
        {
            var actualDistance = (_enemyTransform.position - _playerTransform.position).sqrMagnitude;
            var expectedDistance = compareValue.Value * compareValue.Value;
            return compareType.Value switch
            {
                ECompareType.Less => actualDistance < expectedDistance ? ENodeState.Success : ENodeState.Failure,
                ECompareType.More => actualDistance > expectedDistance ? ENodeState.Success : ENodeState.Failure,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}