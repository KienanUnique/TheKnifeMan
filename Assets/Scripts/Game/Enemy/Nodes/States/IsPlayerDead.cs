using System;
using Game.Player;
using Zenject;

namespace Game.Enemy.Nodes.States
{
    [Serializable]
    public class IsPlayerDead : AAiActionNode
    {
        [Inject] private IPlayerInformation _playerInformation;

        protected override ENodeState OnUpdate()
        {
            return _playerInformation.IsDead.Value ? ENodeState.Success : ENodeState.Failure;
        }
    }
}