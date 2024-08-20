using KoboldUi.Windows;
using Ui.Splash.Logo;
using UnityEngine;

namespace Ui.Splash
{
    public class SplashWindow : AWindow
    {
        [SerializeField] private LogoView logoView;

        protected override void AddControllers()
        {
            AddController<LogoController, LogoView>(logoView);
        }
    }
}