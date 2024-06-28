using System;
using Game.Enemy.ActionsExecutor;

namespace Game.Enemy.Nodes.Actions
{
    [Serializable]
    public class AttackMelee : AAiActionNode<IMeleeAttackEnemy>
    {
        protected override ENodeState OnUpdate()
        {
            Enemy.AttackMelee();
            return ENodeState.Success;
        }
    }
}