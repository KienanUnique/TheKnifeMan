using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Game.Projectile.Pattern.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Projectiles + nameof(SpreadForwardProjectilePattern),
        fileName = nameof(SpreadForwardProjectilePattern))]
    public class SpreadForwardProjectilePattern : AProjectilesPattern
    {
        [SerializeField] private float angle = 30f;
        [SerializeField] private int count = 5;
        
        public override IReadOnlyList<Vector2> Directions
        {
            get
            {
                var directions = new List<Vector2>();
                
                var targetDirection = Vector2.left;
             
                var stepAngle = angle / (count - 1);
                var currentAngle = -angle / 2;

                for (var i = 0; i < count; i++)
                {
                    var direction = Quaternion.Euler(0, 0, currentAngle) * targetDirection;
                    directions.Add(direction.normalized);
                    currentAngle += stepAngle;
                }
                
                return directions;
            }
        }
    }
}