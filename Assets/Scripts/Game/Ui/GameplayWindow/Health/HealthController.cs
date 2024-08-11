using System;
using System.Collections.Generic;
using Db.Player;
using DG.Tweening;
using Game.Player;
using KoboldUi.Element.Controller;
using UniRx;

namespace Game.Ui.GameplayWindow.Health
{
    public class HealthController : AUiController<HealthView>
    {
        private const float VisibleAlpha = 1f;
        private const float InvisibleAlpha = 0f;
        
        private readonly IPlayerInformation _playerInformation;
        private readonly IPlayerParameters _playerParameters;

        private readonly Stack<HealthCell> _activeHealthCells = new();
        private readonly Stack<HealthCell> _inactiveHealthCells = new();
        
        private IDisposable _healthChangeDisposable;
        private Tween _shakeTween;

        public HealthController(IPlayerInformation playerInformation, IPlayerParameters playerParameters)
        {
            _playerInformation = playerInformation;
            _playerParameters = playerParameters;
        }

        public override void Initialize()
        {
            var maxHealth = _playerParameters.Health;
            
            for (var i = 0; i < maxHealth; i++)
            {
                var healthCell = UnityEngine.Object.Instantiate(View.healthCellPrefab, View.cellsContainer, false);
                FillCellWithoutAnimation(healthCell);
                _activeHealthCells.Push(healthCell);
            }
        }

        protected override void OnOpen()
        {
            _healthChangeDisposable?.Dispose();
            _healthChangeDisposable = _playerInformation.Health.Subscribe(OnPlayerHealthChanged).AddTo(View);
        }

        protected override void OnClose()
        {
            _shakeTween?.Kill();
            _healthChangeDisposable?.Dispose();
        }

        private void OnPlayerHealthChanged(int newHealth)
        {
            var isHealthActual = _activeHealthCells.Count == newHealth;
            if(isHealthActual)
                return;
            
            while (_activeHealthCells.Count > newHealth)
            {
                var healthCell = _activeHealthCells.Pop();
                UnFillCell(healthCell);
                _inactiveHealthCells.Push(healthCell);
            }
            
            while (_activeHealthCells.Count < newHealth)
            {
                var healthCell = _inactiveHealthCells.Pop();
                FillCell(healthCell);
                _activeHealthCells.Push(healthCell);
            }
            
            _shakeTween?.Kill(true);
            _shakeTween = View.rootContainer.DOShakeRotation(
                duration: View.shakeAnimationDuration,
                strength: View.shakeAnimationStrength,
                vibrato: View.shakeAnimationVibrato, 
                randomness: View.shakeAnimationRandomness,
                randomnessMode: View.shakeAnimationRandomnessMode, 
                fadeOut: View.shakeAnimationFadeOut
            );
        }

        private void FillCellWithoutAnimation(HealthCell healthCell)
        {
            var normalColor = healthCell.filledCell.color;
            normalColor.a = VisibleAlpha;
            healthCell.filledCell.color = normalColor;
        }

        private void UnFillCell(HealthCell healthCell)
        {
            healthCell.filledCell.DOFade(InvisibleAlpha, View.cellAnimationDuration).SetLink(View.gameObject);
        }
        
        private void FillCell(HealthCell healthCell)
        {
            healthCell.filledCell.DOFade(VisibleAlpha, View.cellAnimationDuration).SetLink(View.gameObject);
        }
    }
}