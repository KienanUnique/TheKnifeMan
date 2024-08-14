using Alchemy.Inspector;
using FinalTitles.Impl;
using Game.CameraHolder.Impl;
using Game.Object.PartsFactory;
using Services;
using UnityEngine;
using Zenject;

namespace Installers.FinalTitles
{
    public class FinalTitlesInstaller : MonoInstaller
    {
        [SerializeField] private FinalTitlesVideoPlayerController finalTitlesVideoPlayerController;
        
        [Header("Assets")] 
        [AssetsOnly] [SerializeField] private CameraHolderController cameraHolderPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FinalTitlesService>().AsSingle().NonLazy();

            InstallCameras();
        }

        private void InstallCameras()
        {
            Container.BindInterfacesTo<EmptyPartsFactory>().AsSingle()
                .WhenInjectedInto(typeof(CameraHolderController), typeof(FinalTitlesVideoPlayerController));

            Container.BindInterfacesAndSelfTo<CameraHolderController>()
                .FromComponentInNewPrefab(cameraHolderPrefab)
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<FinalTitlesVideoPlayerController>()
                .FromInstance(finalTitlesVideoPlayerController)
                .AsSingle()
                .NonLazy();
        }
    }
}