using System;
using UnityEngine;

namespace Game.SpawnPoint
{
    public interface IEnemySpawnPoint
    {
        Vector3 Position { get; }
        IObservable<IEnemySpawnPoint> OnBecomeFree { get; }

        void SetBusy(float duration);
    }
}