using System;
using Game.Object;
using UnityEngine;
using UnityEngine.Rendering;

namespace Installers.MainMenu
{
    [Serializable]
    public class PostProcessingData : AObjectData
    {
        [SerializeField] private Volume fadeVolume;
        
        public Volume FadeVolume => fadeVolume;
    }
}