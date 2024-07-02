using System;
using Alchemy.Inspector;
using UnityEngine;

namespace Utils.Sounds
{
    [Serializable]
    public class AudioClipVo
    {
#if UNITY_EDITOR
        public AudioClip clip;
#endif
        [ReadOnly] public string path;
        [Range(0, 1f)] public float volume = 1f;
    }
}