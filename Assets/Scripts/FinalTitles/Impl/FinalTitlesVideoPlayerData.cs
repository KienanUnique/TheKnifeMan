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
        [SerializeField] private string videoName = "VC_Final.mp4";

        public VideoPlayer VideoPlayer => videoPlayer;
        public string VideoName => videoName;
    }
}