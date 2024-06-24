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
        
        private IDisposable _healthChangeDisposable;

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
            _healthChangeDisposable?.Dispose();
        }

        private void OnPlayerHealthChanged(int newHealth)
        {
            while (_activeHealthCells.Count > newHealth)
            {
                var healthCell = _activeHealthCells.Pop();
                UnFillCell(healthCell);
            }
        }

        private void FillCellWithoutAnimation(HealthCell healthCell)
        {
            var normalColor = healthCell.filledCell.color;
            normalColor.a = VisibleAlpha;
            healthCell.filledCell.color = normalColor;
        }

        private void UnFillCell(HealthCell healthCell)
        {
            healthCell.filledCell.DOFade(InvisibleAlpha, View.cellHideAnimationDuration).SetLink(View.gameObject);
        }
    }
}