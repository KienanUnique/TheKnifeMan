using Alchemy.Inspector;
using DG.Tweening;
using KoboldUi.Element.View;
using UnityEngine;

namespace Game.Ui.GameplayWindow.Health
{
    public class HealthView : AUiAnimatedView
    {
        [Header("Links")]
        public RectTransform cellsContainer;
        public RectTransform rootContainer;
        
        [Header("Cell Hide Animation")]
        public float cellAnimationDuration = 1f;
        
        [Header("Shake Animation")]
        public float shakeAnimationDuration = 1f;
        public Vector3 shakeAnimationStrength = new Vector3(0f, 0f, 10f);
        public int shakeAnimationVibrato = 10;
        public float shakeAnimationRandomness = 90f;
        public ShakeRandomnessMode shakeAnimationRandomnessMode = ShakeRandomnessMode.Full;
        public bool shakeAnimationFadeOut = false;

        [Header("Prefabs")]
        [AssetsOnly] public HealthCell healthCellPrefab;
    }
}