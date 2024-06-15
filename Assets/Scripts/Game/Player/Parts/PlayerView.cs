using Game.Object;
using UnityEngine;

namespace Game.Player.Parts
{
    public class PlayerView : AObjectView
    {
        [SerializeField] private Rigidbody2D mainRigidbody;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer mainSprite;

        public Rigidbody2D MainRigidbody => mainRigidbody;
        public UnityEngine.Animator Animator => animator;
        public SpriteRenderer MainSprite => mainSprite;
    }
}