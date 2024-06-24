using Game.Services.Level;
using KoboldUi.Element.Controller;
using Ui.MainMenu.View;
using UniRx;
using UnityEngine;

namespace Ui.MainMenu.Controller
{
    public class ManMenuController : AUiController<MainMenuView>
    {
        private readonly ILevelsService _levelsService;

        public ManMenuController(ILevelsService levelsService)
        {
            _levelsService = levelsService;
        }

        public override void Initialize()
        {
            View.startButton.OnClickAsObservable().Subscribe(_ => OnStartButtonClicked()).AddTo(View);
            View.quitButton.OnClickAsObservable().Subscribe(_ => OnQuitClicked()).AddTo(View);
        }

        private void OnStartButtonClicked()
        {
            _levelsService.LoadFirstLevel();
        }
        
        private void OnQuitClicked()
        {
            Application.Quit();
        }
    }
}