using Game.Player;
using Game.Player.Parts;
using Game.Player.PartsFactory;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView player;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlayerPartsFactory>().AsSingle().WhenInjectedInto<PlayerController>();
            
            Container.BindInterfacesAndSelfTo<PlayerController>()
                .FromComponentInNewPrefab(player)
                .AsSingle()
                .NonLazy();
        }
    }
}