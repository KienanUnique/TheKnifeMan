using System;

namespace PostProcessing
{
    public interface IPostProcessingController
    {
        void EnterFade(Action onCompleted = null);
        void EnterFadeInstantly();
        void ExitFade(Action onCompleted = null);
    }
}