using Db.PostProcessing;
using Db.PostProcessing.Impl;
using Db.Scenes;
using Db.Scenes.Impl;
using Db.Sounds;
using Db.Sounds.Impl;
using UnityEngine;
using Utils;
using Zenject;

namespace Installers.Project
{
    [CreateAssetMenu(menuName = MenuPathBase.Installers + nameof(ProjectParametersInstaller),
        fileName = nameof(ProjectParametersInstaller))]
    public class ProjectParametersInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ScenesParameters scenesParameters;
        [SerializeField] private PostProcessingParameters postProcessingParameters;
        [SerializeField] private SoundFxBase soundFxBase;
        [SerializeField] private GameDefaultParameters gameDefaultParameters;

        public override void InstallBindings()
        {
            Container.Bind<IScenesParameters>().FromInstance(scenesParameters).AsSingle();
            Container.Bind<IPostProcessingParameters>().FromInstance(postProcessingParameters).AsSingle();
            Container.Bind<ISoundFxBase>().FromInstance(soundFxBase).AsSingle();
            Container.Bind<IGameDefaultParameters>().FromInstance(gameDefaultParameters).AsSingle();
        }
    }
}