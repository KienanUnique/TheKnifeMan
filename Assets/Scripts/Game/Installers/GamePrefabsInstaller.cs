using Alchemy.Inspector;
using Game.CameraHolder;
using Game.CameraHolder.Impl;
using Game.Object.PartsFactory;
using Game.Player;
using Game.Player.PartsFactory;
using Game.Utils;
using Game.VirtualCamera;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [RequireComponent(typeof(LevelViewLink))]
    public class GamePrefabsInstaller : MonoInstaller
    {
        [AssetsOnly] [SerializeField] private PlayerController playerPrefab;
        [AssetsOnly] [SerializeField] private VirtualCameraController virtualCameraPrefab;
        [AssetsOnly] [SerializeField] private CameraHolderController cameraHolderPrefab;

        public override void InstallBindings()
        {
            InstallPlayer();
            InstallCameras();
        }

        private void InstallPlayer()
        {
            Container.BindInterfacesTo<PlayerPartsFactory>().AsSingle().WhenInjectedInto<PlayerController>();

            var levelViewLink = GetComponent<LevelViewLink>();
            var levelView = levelViewLink.LevelView;
            
            Container.BindInterfacesAndSelfTo<PlayerController>()
                .FromComponentInNewPrefab(playerPrefab)
                .AsSingle()
                .OnInstantiated((_, o) =>
                {
                    var player = o as PlayerController;
                    player.transform.position = levelView.PlayerSpawnPoint;
                })
                .NonLazy();
        }

        private void InstallCameras()
        {
            Container.BindInterfacesTo<EmptyPartsFactory>().AsSingle()
                .WhenInjectedInto(typeof(VirtualCameraController), typeof(CameraHolderController));

            Container.BindInterfacesAndSelfTo<VirtualCameraController>()
                .FromComponentInNewPrefab(virtualCameraPrefab)
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<CameraHolderController>()
                .FromComponentInNewPrefab(cameraHolderPrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}