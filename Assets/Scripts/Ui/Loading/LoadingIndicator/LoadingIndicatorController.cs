using KoboldUi.Element.Controller;
using Services.Level;
using UniRx;
using UnityEngine;

namespace Ui.Loading.LoadingIndicator
{
    public class LoadingIndicatorController : AUiController<LoadingIndicatorView>
    {
        private readonly ILevelsService _levelsService;

        public LoadingIndicatorController(ILevelsService levelsService)
        {
            _levelsService = levelsService;
        }

        public override void Initialize()
        {
            _levelsService.LoadingProgress.Subscribe(OnLoadingProgress).AddTo(View);
        }

        private void OnLoadingProgress(float progress)
        {
            var progressPercentage = (int) (progress * 100f);
            progressPercentage = Mathf.Clamp(progressPercentage, 0, 100);
            
            View.loadingProgressText.text = $"{progressPercentage}%";
        }
    }
}