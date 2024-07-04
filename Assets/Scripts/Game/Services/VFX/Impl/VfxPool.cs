using System.Collections.Generic;
using Db.Vfx;
using Game.Utils;
using UnityEngine;

namespace Game.Services.VFX.Impl
{
    public class VfxPool
    {
        private readonly Transform _rootTransform;
        private readonly EVfxType _vfxType;
        private readonly IVfxBase _vfxBase;
        
        private readonly Stack<ParticleSystem> _pool = new();

        public VfxPool(
            IVfxBase vfxBase, 
            Transform rootTransform, 
            EVfxType vfxType
        )
        {
            _rootTransform = rootTransform;
            _vfxType = vfxType;
            _vfxBase = vfxBase;
        }

        public ParticleSystem Get()
        {
            ParticleSystem vfx;

            while (_pool.TryPop(out vfx) && vfx == null)
            {
            }
            
            if (_pool.Count == 0 && vfx == null)
            {
                CreatePoolElement();
                vfx = _pool.Pop();
            }
            
            vfx.gameObject.SetActive(true);

            return vfx;
        }

        public void Return(ParticleSystem vfx)
        {
            vfx.gameObject.SetActive(false);

            vfx.Stop();

            var sourceTransform = vfx.transform;
            sourceTransform.SetParent(_rootTransform);
            sourceTransform.position = Vector3.zero;

            _pool.Push(vfx);
        }
        
        public void CreatePoolElement()
        {
            var prefab = _vfxBase.GetVfx(_vfxType);
            var newVfx = UnityEngine.Object.Instantiate(prefab, _rootTransform);

            Return(newVfx);
        }
    }
}