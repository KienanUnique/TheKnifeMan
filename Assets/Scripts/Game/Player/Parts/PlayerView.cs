using Game.Object;
using UnityEngine;

namespace Game.Player.Parts
{
    public class PlayerView : AObjectView
    {
        [SerializeField] private Rigidbody2D mainRigidbody;

        public Rigidbody2D MainRigidbody => mainRigidbody;
    }
}