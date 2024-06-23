using System;
using Game.Enemy.ActionsExecutor;
using Game.Projectile.Pattern;

namespace Game.Enemy.Nodes.Actions
{
    [Serializable]
    public class AttackWithProjectile : AAiActionNode<IProjectileAttackEnemy>
    {
        public NodeProperty<AProjectilesPattern> pattern = new();

        protected override ENodeState OnUpdate()
        {
            Enemy.AttackWithProjectile(pattern.Value);
            return ENodeState.Success;
        }
    }
}