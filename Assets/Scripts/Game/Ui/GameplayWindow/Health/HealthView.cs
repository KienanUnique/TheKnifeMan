using Alchemy.Inspector;
using KoboldUi.Element.View;
using UnityEngine;

namespace Game.Ui.GameplayWindow.Health
{
    public class HealthView : AUiAnimatedView
    {
        public RectTransform cellsContainer;
        public float cellHideAnimationDuration;
        
        [AssetsOnly] public HealthCell healthCellPrefab;
    }
}