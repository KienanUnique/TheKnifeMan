using KoboldUi.Element.Controller;
using Services.Level;
using UniRx;
using UnityEngine;

namespace Ui.MainMenu.MainMenu
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