using Game.Ui.GameplayWindow;
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

        public override void InstallBindings()
        {
            var canvasInstance = Instantiate(canvas);

            Container.BindWindowFromPrefab(canvasInstance, gameplayWindow);
        }
    }
}