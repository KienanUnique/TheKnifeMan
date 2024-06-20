using UnityEngine;

namespace Game.Enemy.Data
{
    public interface IMeleeEnemyData : IEnemyData
    {
        BoxCollider2D DamageColliderUp { get; }
        BoxCollider2D DamageColliderDown { get; }
        BoxCollider2D DamageColliderLeft { get; }
        BoxCollider2D DamageColliderRight { get; }
    }
}