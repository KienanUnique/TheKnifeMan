using System;
using Game.Player;
using Game.Utils;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Nodes.States
{
    [Serializable]
    public class DistanceToPlayer : AAiActionNode
    {
        [SerializeField] private ECompareType compareType = ECompareType.Less;
        [SerializeField] private float compareValue = 1f;

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
            var expectedDistance = compareValue * compareValue;
            return compareType switch
            {
                ECompareType.Less => actualDistance < expectedDistance ? ENodeState.Success : ENodeState.Failure,
                ECompareType.More => actualDistance > expectedDistance ? ENodeState.Success : ENodeState.Failure,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}