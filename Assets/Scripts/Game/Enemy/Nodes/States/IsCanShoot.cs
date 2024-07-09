using System;
using Game.Enemy.ActionsExecutor;

namespace Game.Enemy.Nodes.States
{
    [Serializable]
    public class IsCanShoot : AAiActionNode<IProjectileAttackEnemy>
    {
        protected override ENodeState OnUpdate()
        {
            return Enemy.IsCanShoot ? ENodeState.Success : ENodeState.Failure;
        }
    }
}