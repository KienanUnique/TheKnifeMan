using Game.CameraHolder;
using Game.Object.PartsFactory;
using KoboldUi.Utils;
using Ui.MainMenu;
using UnityEngine;
using Utils;
using Zenject;

namespace Installers.MainMenu
{
    [CreateAssetMenu(menuName = MenuPathBase.Installers + nameof(MainMenuInstaller), fileName = nameof(MainMenuInstaller))]
    public class MainMenuInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CameraHolderController cameraHolderPrefab;
        [SerializeField] private MainMenuWindow menuWindow;

        public override void InstallBindings()
        {
            var canvasInstance = Instantiate(canvas);
            
            Container.BindWindowFromPrefab(canvasInstance, menuWindow);
            
            Container.BindInterfacesTo<MainMenuOpener>().AsSingle().NonLazy();

            InstallCameras();
        }

        private void InstallCameras()
        {
            Container.BindInterfacesTo<EmptyPartsFactory>().AsSingle()
                .WhenInjectedInto(typeof(CameraHolderController));

            Container.BindInterfacesAndSelfTo<CameraHolderController>()
                .FromComponentInNewPrefab(cameraHolderPrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}