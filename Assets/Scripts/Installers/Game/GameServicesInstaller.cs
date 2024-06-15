using Services.ScreenPosition.Impl;
using Zenject;

namespace Installers.Game
{
    public class GameServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ScreenPositionService>().AsSingle();
        }
    }
}