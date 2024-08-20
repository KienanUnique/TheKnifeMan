using System;
using Game.Enemy.ActionsExecutor;

namespace Game.Enemy.Nodes.Actions
{
    [Serializable]
    public class AttackWithProjectile : AAiActionNode<IProjectileAttackEnemy>
    {
        protected override ENodeState OnUpdate()
        {
            Enemy.AttackWithProjectile();
            return ENodeState.Success;
        }
    }
}