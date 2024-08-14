using System;
using Game.Object;
using UnityEngine;
using UnityEngine.Rendering;

namespace PostProcessing.Impl
{
    [Serializable]
    public class PostProcessingData : AObjectData
    {
        [SerializeField] private Volume fadeVolume;
        
        public Volume FadeVolume => fadeVolume;
    }
}