using System;
using UniRx;
using UnityEngine;

namespace Game.SpawnPoint.Impl
{
    public class EnemySpawnPoint : MonoBehaviour, IEnemySpawnPoint
    {
        private readonly ReactiveCommand<IEnemySpawnPoint> _onBecomeFree = new();

        public Vector3 Position => transform.position;
        public IObservable<IEnemySpawnPoint> OnBecomeFree => _onBecomeFree;

        public void SetBusy(float duration)
        {
            Observable.Timer(TimeSpan.FromSeconds(duration)).Subscribe(_ => OnSpawnDelayPassed()).AddTo(this);
        }

        private void OnSpawnDelayPassed()
        {
            _onBecomeFree.Execute(this);
        }
    }
}