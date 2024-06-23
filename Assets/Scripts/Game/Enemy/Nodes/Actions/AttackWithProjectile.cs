using System;
using Game.Enemy.ActionsExecutor;
using Game.Projectile.Pattern;
using Game.Projectile.TypeData.Impl;

namespace Game.Enemy.Nodes.Actions
{
    [Serializable]
    public class AttackWithProjectile : AAiActionNode<IProjectileAttackEnemy>
    {
        public NodeProperty<AProjectilesPattern> pattern = new();
        public NodeProperty<ProjectileTypeData> type = new();

        protected override ENodeState OnUpdate()
        {
            Enemy.AttackWithProjectile(pattern.Value, type.Value);
            return ENodeState.Success;
        }
    }
}