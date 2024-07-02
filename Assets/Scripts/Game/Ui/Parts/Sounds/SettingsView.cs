using KoboldUi.Element.View;
using UnityEngine.UI;

namespace Game.Ui.Parts.Sounds
{
    public class SettingsView : AUiAnimatedView
    {
        public Toggle soundToggle;
        public Slider soundVolume;
        
        public Toggle musicToggle;
        public Slider musicVolume;
        
        public Button playTestSound;
    }
}