using System;
using System.Collections.Generic;
using Db.EnemySpawnFx;
using Game.Utils;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Services.SpawnEffects.SpawnEffects.Impl
{
    public class EnemySpawnEffectsService : IEnemySpawnEffectsService, IInitializable
    {
        private readonly IEnemySpawnFxBase _spawnFxBase;
        private readonly List<EnemySpawnEffectsPool> _pools = new();

        public EnemySpawnEffectsService(IEnemySpawnFxBase spawnFxBase)
        {
            _spawnFxBase = spawnFxBase;
        }

        public void Initialize()
        {
            var rootTransform = new GameObject("SpawnFx").transform;
            foreach (var effect in _spawnFxBase.AllEffects)
            {
                var effectPool = new EnemySpawnEffectsPool(rootTransform, effect);
                _pools.Add(effectPool);
            }
        }

        public IObservable<Unit> PlayEffect(Vector3 position)
        {
            var randomEffectPoolIndex = Random.Range(0, _pools.Count - 1);
            var randomEffectPool = _pools[randomEffectPoolIndex];

            var spawnEffectData = randomEffectPool.Get();

            spawnEffectData.EndTrigger.SpawnEffectEnded.Subscribe(_ =>
            {
                spawnEffectData.Animator.ResetTrigger(AnimationKeys.PlaySpawnAnimation);
                randomEffectPool.Return(spawnEffectData);
            }).AddTo(spawnEffectData.Animator);
            
            spawnEffectData.Animator.transform.position = position;
            spawnEffectData.Animator.SetTrigger(AnimationKeys.PlaySpawnAnimation);

            return spawnEffectData.SpawnTrigger.SpawnFramePlayed;
        }
    }
}