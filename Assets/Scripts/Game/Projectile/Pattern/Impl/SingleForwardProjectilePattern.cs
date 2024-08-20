using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Game.Projectile.Pattern.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Projectiles + nameof(SingleForwardProjectilePattern),
        fileName = nameof(SingleForwardProjectilePattern))]
    public class SingleForwardProjectilePattern : AProjectilesPattern
    {
        private readonly List<Vector2> _directions = new() {Vector2.left};

        public override IReadOnlyList<Vector2> Directions => _directions;
    }
}