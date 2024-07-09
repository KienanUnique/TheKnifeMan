using System;
using Game.Player;
using UnityEngine.AI;
using Zenject;

namespace Game.Enemy.Nodes.States
{
    [Serializable]
    public class IsPathToPlayerClear : AAiActionNode
    {
        [Inject] private IPlayerInformation _information;

        protected override ENodeState OnUpdate()
        {
            var thisPosition = ConcreteContext.Transform.position;
            var playerPosition = _information.Transform.position;
            
            thisPosition.z = 0;
            playerPosition.z = 0;

            var isPathWithObstacles = NavMesh.Raycast(thisPosition, playerPosition, out _, NavMesh.AllAreas);

            return isPathWithObstacles ? ENodeState.Failure : ENodeState.Success;
        }
    }
}