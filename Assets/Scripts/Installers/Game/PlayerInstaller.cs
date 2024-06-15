using Game.Level;
using Game.Player;
using Game.Player.Parts;
using Game.Player.PartsFactory;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private LevelView levelView;
        
        [Header("Prefab only!")]
        [SerializeField] private PlayerView playerPrefab;

        public override void InstallBindings()
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
    }
}