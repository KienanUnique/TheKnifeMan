using Services.Input.Impl;
using Zenject;

namespace Installers.Project
{
    public class ProjectServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputService>().AsSingle();
        }
    }
}