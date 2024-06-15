using System;
using Game.Object;
using UnityEngine;

namespace Game.Player
{
    [Serializable]
    public class PlayerData : AObjectData
    {
        [SerializeField] private Rigidbody2D mainRigidbody;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer mainSprite;

        public Rigidbody2D MainRigidbody => mainRigidbody;
        public Animator Animator => animator;
        public SpriteRenderer MainSprite => mainSprite;
    }
}