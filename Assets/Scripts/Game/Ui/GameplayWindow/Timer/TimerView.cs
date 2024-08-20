using KoboldUi.Element.View;
using TMPro;
using UnityEngine;

namespace Game.Ui.GameplayWindow.Timer
{
    public class TimerView : AUiAnimatedView
    {
        public TMP_Text timerText;

        [Header("Timer End Animation")] 
        public int countOfBlinks;
        public float visibleDelay;
        public Color timerEndColor;
    }
}