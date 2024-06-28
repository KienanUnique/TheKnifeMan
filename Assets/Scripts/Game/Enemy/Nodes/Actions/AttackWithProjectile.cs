using System;
using Game.Enemy.ActionsExecutor;
using Game.Projectile.Pattern;
using Game.Projectile.TypeData.Impl;
using UnityEngine;

namespace Game.Enemy.Nodes.Actions
{
    [Serializable]
    public class AttackWithProjectile : AAiActionNode<IProjectileAttackEnemy>
    {
        [SerializeField] private AProjectilesPattern pattern;
        [SerializeField] private ProjectileTypeData type;

        protected override ENodeState OnUpdate()
        {
            Enemy.AttackWithProjectile(pattern, type);
            return ENodeState.Success;
        }
    }
}