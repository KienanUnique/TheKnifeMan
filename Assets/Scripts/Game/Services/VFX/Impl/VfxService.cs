using System;
using System.Collections.Generic;
using Db.Vfx;
using Game.Utils;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Services.VFX.Impl
{
    public class VfxService : IVfxService, IInitializable
    {
        private readonly IVfxBase _vfxBase;

        private readonly Dictionary<EVfxType, VfxPool> _vfxPools = new();

        private Transform _rootTransform;

        public VfxService(IVfxBase vfxBase)
        {
            _vfxBase = vfxBase;
        }

        public void Initialize()
        {
            _rootTransform = new GameObject("Vfx").transform;
        }

        public void Play(EVfxType vfxType, Transform root, Action callback = null)
        {
            Play(vfxType, vfx => vfx.transform.SetParent(root), callback);
        }

        public void Play(EVfxType vfxType, Vector3 position, Action callback = null)
        {
            Play(vfxType, vfx => vfx.transform.position = position, callback);
        }

        private void Play(EVfxType vfxType, Action<ParticleSystem> prepareAction, Action callback)
        {
            var vfxPool = GetVfxPool(vfxType);
            var vfx = vfxPool.Get();

            prepareAction.Invoke(vfx);

            vfx.Play();

            Observable.EveryUpdate()
                .Where(_ => !vfx.isPlaying)
                .Take(1)
                .Subscribe(_ =>
                {
                    vfxPool.Return(vfx);
                    callback?.Invoke();
                })
                .AddTo(vfx);
        }

        private VfxPool GetVfxPool(EVfxType vfxType)
        {
            if (_vfxPools.ContainsKey(vfxType))
                return _vfxPools[vfxType];

            var newVfxPool = new VfxPool(_vfxBase, _rootTransform, vfxType);
            _vfxPools.Add(vfxType, newVfxPool);

            return newVfxPool;
        }
    }
}