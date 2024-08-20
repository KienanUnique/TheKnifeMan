using System;
using Game.Utils;
using UnityEngine;

namespace Game.Services.VFX
{
    public interface IVfxService
    {
        void Play(EVfxType vfxType, Transform root, Action callback = null);
        void Play(EVfxType vfxType, Vector3 position, Action callback = null);
    }
}