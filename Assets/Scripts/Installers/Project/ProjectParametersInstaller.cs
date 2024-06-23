using Db.Scenes;
using Db.Scenes.Impl;
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

        public override void InstallBindings()
        {
            Container.Bind<IScenesParameters>().FromInstance(scenesParameters).AsSingle();
        }
    }
}