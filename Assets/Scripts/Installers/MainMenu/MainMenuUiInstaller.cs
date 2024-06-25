using KoboldUi.Utils;
using Ui.MainMenu;
using UnityEngine;
using Utils;
using Zenject;

namespace Installers.MainMenu
{
    [CreateAssetMenu(menuName = MenuPathBase.Installers + nameof(MainMenuUiInstaller), fileName = nameof(MainMenuUiInstaller))]
    public class MainMenuUiInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private MainMenuWindow menuWindow;

        public override void InstallBindings()
        {
            var canvasInstance = Instantiate(canvas);
            
            Container.BindWindowFromPrefab(canvasInstance, menuWindow);

            Container.BindInterfacesTo<MainMenuOpener>().AsSingle().NonLazy();
        }
    }
}