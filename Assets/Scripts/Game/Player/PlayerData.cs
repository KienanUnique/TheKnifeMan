using System;
using System.Linq;
using Alchemy.Inspector;
using Game.Character;
using UnityEngine;

namespace Game.Player
{
    [Serializable]
    public class PlayerData : ACharacterData
    {
        [SerializeField] private Rigidbody2D mainRigidbody;
        
        [Header("Damage colliders")]
        [SerializeField] private BoxCollider2D damageColliderUp;
        [SerializeField] private BoxCollider2D damageColliderDown;
        [SerializeField] private BoxCollider2D damageColliderLeft;
        [SerializeField] private BoxCollider2D damageColliderRight;

        public Rigidbody2D MainRigidbody => mainRigidbody;

        public BoxCollider2D DamageColliderUp => damageColliderUp;
        public BoxCollider2D DamageColliderDown => damageColliderDown;
        public BoxCollider2D DamageColliderLeft => damageColliderLeft;
        public BoxCollider2D DamageColliderRight => damageColliderRight;
        
        [Button]
        public override void AutoFill()
        {
            base.AutoFill();
            mainRigidbody = rootTransform.GetComponent<Rigidbody2D>();

            var colliders = rootTransform.GetComponentsInChildren<BoxCollider2D>().ToList();
            damageColliderUp = colliders.FirstOrDefault(collider => collider.name.Contains("Up"));
            damageColliderDown = colliders.FirstOrDefault(collider => collider.name.Contains("Down"));
            damageColliderLeft = colliders.FirstOrDefault(collider => collider.name.Contains("Left"));
            damageColliderRight = colliders.FirstOrDefault(collider => collider.name.Contains("Right"));
        }
    }
}