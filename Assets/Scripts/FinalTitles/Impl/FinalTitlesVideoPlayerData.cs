using System;
using Game.Object;
using UnityEngine;
using UnityEngine.Video;

namespace FinalTitles.Impl
{
    [Serializable]
    public class FinalTitlesVideoPlayerData : AObjectData
    {
        [SerializeField] private VideoPlayer videoPlayer;

        public VideoPlayer VideoPlayer => videoPlayer;
    }
}