using Game.Ui.GameplayWindow;
using Game.Ui.LoseWindow;
using Game.Ui.PauseWindow;
using Game.Ui.WinWindow;
using KoboldUi.Utils;
using UnityEngine;
using Utils;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(menuName = MenuPathBase.Installers + nameof(GameUiInstaller), fileName = nameof(GameUiInstaller))]
    public class GameUiInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private GameplayWindow gameplayWindow;
        [SerializeField] private PauseWindow pauseWindow;
        [SerializeField] private LoseWindow loseWindow;
        [SerializeField] private WinWindow winWindow;

        public override void InstallBindings()
        {
            var canvasInstance = Instantiate(canvas);

            Container.BindWindowFromPrefab(canvasInstance, gameplayWindow);
            Container.BindWindowFromPrefab(canvasInstance, loseWindow);
            Container.BindWindowFromPrefab(canvasInstance, winWindow);
            Container.BindWindowFromPrefab(canvasInstance, pauseWindow);
        }
    }
}