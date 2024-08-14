using System;
using UniRx;

namespace FinalTitles
{
    public interface IFinalTitlesVideoPlayerController
    {
        IObservable<Unit> VideoEnded { get; }
        void Play();
    }
}