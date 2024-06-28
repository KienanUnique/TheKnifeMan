using DG.Tweening;
using UnityEngine;
using Utils;

namespace Db.PostProcessing.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(PostProcessingParameters),
        fileName = nameof(PostProcessingParameters))]
    public class PostProcessingParameters : ScriptableObject, IPostProcessingParameters
    {
        [SerializeField] private Ease changeLayerWeightEase;
        [SerializeField] private float fadeEnterDuration;
        [SerializeField] private float fadeExitDuration;

        public Ease ChangeLayerWeightEase => changeLayerWeightEase;
        public float FadeEnterDuration => fadeEnterDuration;
        public float FadeExitDuration => fadeExitDuration;
    }
}