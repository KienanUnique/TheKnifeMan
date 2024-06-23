using System;
using UnityEngine;

namespace Game.Projectile.Controller
{
    public interface IPoolProjectile
    {
        IObservable<IPoolProjectile> Disappeared { get; }

        void Appear(Vector2 direction, Vector2 position, IProjectilesSender sender);
        void DisableAndReset();
    }
}