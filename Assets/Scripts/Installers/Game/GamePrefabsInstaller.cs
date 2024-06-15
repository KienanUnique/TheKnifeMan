using Game.Camera;
using Game.Level;
using Game.Object.PartsFactory;
using Game.Player;
using Game.Player.PartsFactory;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    public class GamePrefabsInstaller : MonoInstaller
    {
        [SerializeField] private LevelView levelView;
        
        [Header("Prefab only!")]
        [SerializeField] private PlayerController playerPrefab;
        
        [SerializeField] private CameraController cameraPrefab;

        public override void InstallBindings()
        {
            InstallPlayer();
            InstallCamera();
        }

        private void InstallPlayer()
        {
            Container.BindInterfacesTo<PlayerPartsFactory>().AsSingle().WhenInjectedInto<PlayerController>();
            
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

        private void InstallCamera()
        {
            Container.BindInterfacesTo<EmptyPartsFactory>().AsSingle().WhenInjectedInto<CameraController>();
            
            Container.BindInterfacesAndSelfTo<CameraController>()
                .FromComponentInNewPrefab(cameraPrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}