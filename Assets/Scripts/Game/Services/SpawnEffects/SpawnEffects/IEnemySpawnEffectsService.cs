using System;
using UniRx;
using UnityEngine;

namespace Game.Services.SpawnEffects.SpawnEffects
{
    public interface IEnemySpawnEffectsService
    {
        IObservable<Unit> PlayEffect(Vector3 position);
    }
}