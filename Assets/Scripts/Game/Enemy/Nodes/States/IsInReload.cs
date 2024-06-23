using System;
using Game.Enemy.ActionsExecutor;

namespace Game.Enemy.Nodes.States
{
    [Serializable]
    public class IsInReload : AAiActionNode<IProjectileAttackEnemy>
    {
        protected override ENodeState OnUpdate()
        {
            return Enemy.IsInReload ? ENodeState.Success : ENodeState.Failure;
        }
    }
}