using Game.Player;
using Game.Player.Parts;
using Game.Player.Parts.Movement;
using Zenject;

namespace Game.ObjectFactory.Impl
{
    public class PlayerFactory : AObjectFactory<PlayerController, PlayerView>
    {
        public PlayerFactory(DiContainer container, UnityEngine.Object prefab) : base(container, prefab)
        {
        }

        protected override void AddModules(DiContainer container)
        {
            container.BindInterfacesTo<PlayerMovementPart>().AsSingle();
        }
    }
}