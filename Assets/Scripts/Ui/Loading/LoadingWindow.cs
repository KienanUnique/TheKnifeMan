using KoboldUi.Windows;
using Ui.Loading.Loading;
using Ui.Loading.LoadingIndicator;
using UnityEngine;

namespace Ui.Loading
{
    public class LoadingWindow : AWindow
    {
        [SerializeField] private LoadingView loadingView;
        [SerializeField] private LoadingIndicatorView loadingIndicatorView;
        
        protected override void AddControllers()
        {
            AddController<LoadingIndicatorController, LoadingIndicatorView>(loadingIndicatorView);
            AddController<LoadingController, LoadingView>(loadingView);
        }
    }
}