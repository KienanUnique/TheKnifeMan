using KoboldUi.Element.View;
using TMPro;
using UnityEngine;

namespace Ui.MainMenu.Ticker
{
    public class TickerView : AUiAnimatedView
    {
        [Header("Settings")]
        public float speed = 100f;
        [TextArea] public string text;
        
        [Header("Links")]
        public TMP_Text textMeshPro;
        public RectTransform container;
    }
}