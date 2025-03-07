﻿using Game.Utils.Directions;
using UnityEngine;

namespace Game.Enemy.Parts.LookDirection
{
    public interface IProjectileEnemyAttackDirectionPart : IEnemyPoolPart
    {
        (Vector2, EDirection1D) CalculateAttackDirection1D();
    }
}