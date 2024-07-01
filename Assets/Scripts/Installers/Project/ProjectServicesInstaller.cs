using Db.Sounds.Impl;
using Services.Input.Impl;
using Services.Level.Impl;
using Services.Settings.Impl;
using Services.Sound.Impl;
using Zenject;

namespace Installers.Project
{
    public class ProjectServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputService>().AsSingle();
            Container.BindInterfacesTo<LevelsService>().AsSingle();
            Container.BindInterfacesTo<SettingsStorageService>().AsSingle();
            Container.BindInterfacesTo<AudioSourcePool>().AsTransient();
            Container.BindInterfacesTo<UiSoundFxService>().AsSingle();
            Container.BindInterfacesTo<AudioClipRepository>().AsSingle();
        }
    }
}