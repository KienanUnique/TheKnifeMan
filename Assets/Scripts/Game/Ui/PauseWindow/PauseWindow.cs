using Game.Ui.Parts.Sounds;
using Game.Ui.PauseWindow.Pause;
using KoboldUi.Windows;
using UnityEngine;

namespace Game.Ui.PauseWindow
{
    public class PauseWindow : AWindow
    {
        [SerializeField] private PauseView pauseView;
        [SerializeField] private SettingsView settingsView;
        
        protected override void AddControllers()
        {
            AddController<PauseController, PauseView>(pauseView);
            AddController<SettingsController, SettingsView>(settingsView);
        }
    }
}