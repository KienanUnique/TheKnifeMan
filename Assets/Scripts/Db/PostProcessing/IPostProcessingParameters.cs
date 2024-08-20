using DG.Tweening;

namespace Db.PostProcessing
{
    public interface IPostProcessingParameters
    {
        Ease ChangeLayerWeightEase { get; }
        float FadeEnterDuration { get; }
        float FadeExitDuration { get; }
    }
}