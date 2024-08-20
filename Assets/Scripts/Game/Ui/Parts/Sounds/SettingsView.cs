using KoboldUi.Element.View;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.Parts.Sounds
{
    public class SettingsView : AUiAnimatedView
    {
        [Header("Sounds")]
        public Toggle soundToggle;
        public Slider soundVolume;
        public Button playTestSound;
        
        [Header("Music")]
        public Toggle musicToggle;
        public Slider musicVolume;
        
        [Header("Difficulty")]
        public Toggle easyModeToggle;
    }
}