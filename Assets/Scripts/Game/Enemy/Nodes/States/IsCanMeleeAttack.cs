using System;
using Game.Enemy.ActionsExecutor;

namespace Game.Enemy.Nodes.States
{
    [Serializable]
    public class IsCanMeleeAttack : AAiActionNode<IMeleeAttackEnemy>
    {
        protected override ENodeState OnUpdate()
        {
            return Enemy.IsCanMeleeAttack ? ENodeState.Success : ENodeState.Failure;
        }
    }
}