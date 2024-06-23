using System;
using Game.Player;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Game.Enemy.Nodes.Actions
{
    [Serializable]
    public class KeepDistanceFromPlayer : AAiActionNode
    {
        private const float AngleCheckStep = 15f;
        private const float FullCircleAngle = 360f;
        private const float SamplePositionMaxDistance = 1f;
        
        [SerializeField] private float needDistance = 4f;
        
        [Inject] private IPlayerInformation _information;
        
        private Transform _playerTransform;
        private Transform _thisTransform;

        protected override void Initialize()
        {
            base.Initialize();
            _playerTransform = _information.Transform;
            _thisTransform = ConcreteContext.Transform;
        }

        protected override ENodeState OnUpdate()
        {
            if (TryFoundPointOnGivenDistance(out var foundedPoint))
            {
                Enemy.SetDestination(foundedPoint);
                return ENodeState.Success;
            }

            Enemy.SetDestination(_playerTransform.position);
            return ENodeState.Failure;
        }

        private bool TryFoundPointOnGivenDistance(out Vector3 resultPoint)
        {
            var thisPosition = _thisTransform.position;
            var targetPosition = _playerTransform.position;
            
            var targetDirection = (targetPosition - thisPosition).normalized;

            for (var angle = 0f; angle < FullCircleAngle; angle += AngleCheckStep)
            {
                var offsetVector = Quaternion.Euler(0, 0, angle) * -targetDirection * needDistance;
                var point = targetPosition + offsetVector;
                
                if (IsPathFromPointToEnemyClear(point))
                {
                    resultPoint = point;
                    return true;
                }
                
                offsetVector = Quaternion.Euler(0, -angle, 0) * targetDirection * needDistance;
                point = targetPosition + offsetVector;

                if (IsPathFromPointToEnemyClear(point))
                {
                    resultPoint = point;
                    return true;
                }
            }
            
            resultPoint = Vector3.zero;
            return false;
        }

        private bool IsPathFromPointToEnemyClear(Vector3 point)
        {
            if (!NavMesh.SamplePosition(point, out var navMeshHit, SamplePositionMaxDistance, NavMesh.AllAreas))
                return false;
            
            return !NavMesh.Raycast(navMeshHit.position, _playerTransform.position, out _, NavMesh.AllAreas);
        }
    }
}