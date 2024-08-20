using System.Collections.Generic;
using Game.Utils.AnimatorTriggers;
using UnityEngine;

namespace Game.Utils
{
    public class EnemySpawnEffectsPool
    {
        private readonly Transform _rootTransform;
        private readonly Animator _prefab;

        private readonly Stack<Animator> _pool = new();

        public EnemySpawnEffectsPool(
            Transform rootTransform, 
            Animator prefab
        )
        {
            _rootTransform = rootTransform;
            _prefab = prefab;
        }

        public SpawnEffectData Get()
        {
            Animator animator;

            while (_pool.TryPop(out animator))
            {
            }
            
            if (_pool.Count == 0)
            {
                CreatePoolElement();
                animator = _pool.Pop();
            }
            
            animator.gameObject.SetActive(true);

            var spawnEffectEnded = animator.GetBehaviour<SpawnEffectEndedTrigger>();
            var spawnMomentTrigger = animator.GetComponent<SpawnMomentTrigger>();
            
            return new SpawnEffectData(animator, spawnEffectEnded, spawnMomentTrigger);
        }

        public void Return(SpawnEffectData spawnEffectData)
        {
            var animator = spawnEffectData.Animator;
            Return(animator);
        }
        
        private void Return(Animator animator)
        {
            animator.gameObject.SetActive(false);
            
            var sourceTransform = animator.transform;
            sourceTransform.SetParent(_rootTransform);
            sourceTransform.position = Vector3.zero;

            _pool.Push(animator);
        }
        
        private void CreatePoolElement()
        {
            var animator = UnityEngine.Object.Instantiate(_prefab, _rootTransform);
            Return(animator);
        }
    }
}