using DG.Tweening;
using KoboldUi.Element.View;
using UnityEngine;

namespace Ui.MainMenu.GameTitle
{
    public class GameTitleView : AUiAnimatedView
    {
        [Header("Settings")] 
        public Vector3 scalePunch;
        public float duration;
        public int vibrato;
        public float elasticity;
        public Ease ease;

        [Header("Links")]
        public RectTransform container;
    }
}