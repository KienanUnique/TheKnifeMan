using Game.Ui.PauseWindow.Pause;
using KoboldUi.Windows;
using UnityEngine;

namespace Game.Ui.PauseWindow
{
    public class PauseWindow : AWindow
    {
        [SerializeField] private PauseView pauseView;
        
        protected override void AddControllers()
        {
            AddController<PauseController, PauseView>(pauseView);
        }
    }
}