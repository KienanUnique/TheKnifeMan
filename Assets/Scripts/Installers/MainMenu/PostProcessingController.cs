using System;
using Db.PostProcessing;
using DG.Tweening;
using Game.Object;
using PostProcessing;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Installers.MainMenu
{
    public class PostProcessingController : AObjectController<PostProcessingData>, IPostProcessingController
    {
        private const float DisabledVolumeWeight = 0f;
        private const float EnabledVolumeWeight = 1f;

        [SerializeField] private PostProcessingData data;

        [Inject] private IPostProcessingParameters _postProcessingParameters;

        protected override PostProcessingData Data => data;

        public void EnterFade(Action onCompleted)
        {
            EnableVolume(data.FadeVolume, _postProcessingParameters.FadeEnterDuration, onCompleted);
        }

        public void EnterFadeInstantly()
        {
            EnableVolumeInstantly(data.FadeVolume);
        }

        public void ExitFade(Action onCompleted)
        {
            DisableVolume(data.FadeVolume, _postProcessingParameters.FadeExitDuration, onCompleted);
        }

        private Tween EnableVolume(Volume volume, float transitionAnimationDuration, Action onComplete)
        {
            volume.enabled = true;
            return ChangeVolumeWeight(volume, transitionAnimationDuration, volume.weight, EnabledVolumeWeight,
                onComplete);
        }

        private Tween DisableVolume(Volume volume, float transitionAnimationDuration, Action onComplete)
        {
            return ChangeVolumeWeight(volume, transitionAnimationDuration, volume.weight, DisabledVolumeWeight,
                onComplete).OnComplete(() =>
            {
                volume.enabled = false;
                onComplete?.Invoke();
            });
        }

        private Tween ChangeVolumeWeight(Volume volume, float transitionAnimationDuration, float startValue,
            float endValue, Action onComplete = null)
        {
            volume.enabled = true;
            return DOVirtual.Float(startValue, endValue, transitionAnimationDuration,
                    currentWeight => volume.weight = currentWeight)
                .SetLink(gameObject)
                .SetEase(_postProcessingParameters.ChangeLayerWeightEase)
                .OnComplete(() => onComplete?.Invoke());
        }

        private void EnableVolumeInstantly(Volume volume)
        {
            volume.weight = EnabledVolumeWeight;
            volume.enabled = true;
        }

        private void DisableVolumeInstantly(Volume volume)
        {
            volume.weight = DisabledVolumeWeight;
            volume.enabled = false;
        }
    }
}