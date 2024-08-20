using Db.Splash;
using Db.Splash.Impl;
using Game.CameraHolder.Impl;
using Game.Object.PartsFactory;
using KoboldUi.Utils;
using Services.Splash;
using Ui.Splash;
using UnityEngine;
using Utils;
using Zenject;

namespace Installers.Splash
{
    [CreateAssetMenu(menuName = MenuPathBase.Installers + nameof(SplashInstaller),
        fileName = nameof(SplashInstaller))]
    public class SplashInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CameraHolderController cameraHolderPrefab;
        [SerializeField] private SplashWindow splashWindow;
        [SerializeField] private SplashParameters splashParameters;

        public override void InstallBindings()
        {
            var canvasInstance = Instantiate(canvas);
            
            Container.BindWindowFromPrefab(canvasInstance, splashWindow);
            
            Container.BindInterfacesTo<SplashService>().AsSingle().NonLazy();
            Container.Bind<ISplashParameters>().FromInstance(splashParameters).AsSingle();

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