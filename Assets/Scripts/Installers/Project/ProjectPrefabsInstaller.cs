using Game.Object.PartsFactory;
using Installers.MainMenu;
using KoboldUi.Utils;
using Ui.Loading;
using UnityEngine;
using Utils;
using Zenject;

namespace Installers.Project
{
    [CreateAssetMenu(menuName = MenuPathBase.Installers + nameof(ProjectPrefabsInstaller),
        fileName = nameof(ProjectPrefabsInstaller))]
    public class ProjectPrefabsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private LoadingWindow loadingWindow;
        [SerializeField] private PostProcessingController processingController;

        public override void InstallBindings()
        {
            InstallPostProcessing();
            InstallUi();
        }

        private void InstallPostProcessing()
        {
            Container.BindInterfacesTo<EmptyPartsFactory>().AsSingle()
                .WhenInjectedInto(typeof(PostProcessingController));
            
            Container.BindInterfacesTo<PostProcessingController>()
                .FromComponentInNewPrefab(processingController)
                .AsSingle()
                .OnInstantiated((_, o) =>
                {
                    var component = o as Component;
                    DontDestroyOnLoad(component.gameObject);
                })
                .NonLazy();
        }
        
        private void InstallUi()
        {
            var canvasInstance = Instantiate(canvas);
            DontDestroyOnLoad(canvasInstance);
            
            Container.BindWindowFromPrefab(canvasInstance, loadingWindow);
        }
    }
}