using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Services.Input.Impl
{
    public class InputService : IInputService, IInitializable, IDisposable
    {
        private readonly ReactiveProperty<bool> _isDashPressed = new();
        private readonly ReactiveCommand _pausePressed = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        private readonly MainControls _controls = new();
        private readonly ReactiveCommand _attackPressed = new();

        public Vector2 NeedDirection => _controls.Gameplay.Movement.ReadValue<Vector2>();
        public Vector2 MousePosition => Mouse.current.position.ReadValue();

        public IReactiveProperty<bool> IsDashPressed => _isDashPressed;
        public IObservable<Unit> PausePressed => _pausePressed;
        public IObservable<Unit> AttackPressed => _attackPressed;

        public void Initialize()
        {
            SubscribeOnGameplayEvents();

            _controls.Gameplay.Enable();
        }

        public void Dispose()
        {
            UnsubscribeOnGameplayEvents();

            _isDashPressed?.Dispose();
            _pausePressed?.Dispose();
            _compositeDisposable?.Dispose();
            _controls?.Dispose();
        }

        #region Gameplay

        private void SubscribeOnGameplayEvents()
        {
            _controls.Gameplay.Attack.performed += OnAttackPerformed;
            _controls.Gameplay.Dash.started += OnDashStarted;
            _controls.Gameplay.Dash.canceled += OnDashCanceled;
        }

        private void UnsubscribeOnGameplayEvents()
        {
            _controls.Gameplay.Attack.performed -= OnAttackPerformed;
            _controls.Gameplay.Dash.started -= OnDashStarted;
            _controls.Gameplay.Dash.canceled -= OnDashCanceled;
        }

        private void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            _attackPressed.Execute();
        }

        private void OnDashStarted(InputAction.CallbackContext obj)
        {
            _isDashPressed.Value = true;
        }

        private void OnDashCanceled(InputAction.CallbackContext obj)
        {
            _isDashPressed.Value = false;
        }

        #endregion
    }
}