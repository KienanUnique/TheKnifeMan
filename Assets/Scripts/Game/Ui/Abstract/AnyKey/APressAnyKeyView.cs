using KoboldUi.Element.View;
using UnityEngine;

namespace Game.Ui.Abstract.AnyKey
{
    public class APressAnyKeyView : AUiAnimatedView
    {
        public RectTransform textContainer;
        [Min(0.5f)] public float appearDelay;


        [Header("Punch Animation")] 
        public Vector3 punchScale;

        public float punchDurationSeconds;
    }
}