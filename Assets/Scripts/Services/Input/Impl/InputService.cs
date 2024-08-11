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
        private readonly ReactiveCommand _anyKeyPressed = new();
        private readonly ReactiveCommand _restartLevelPressed = new();
        private readonly ReactiveCommand _closeWindowPressed = new();

        public Vector2 NeedDirection => _controls.Gameplay.Movement.ReadValue<Vector2>();
        public Vector2 MousePosition => Mouse.current.position.ReadValue();

        public IReactiveProperty<bool> IsDashPressed => _isDashPressed;
        public IObservable<Unit> PausePressed => _pausePressed;
        public IObservable<Unit> CloseWindowPressed => _closeWindowPressed;
        public IObservable<Unit> AttackPressed => _attackPressed;
        public IObservable<Unit> AnyKeyPressed => _anyKeyPressed;
        public IObservable<Unit> RestartLevelPressed => _restartLevelPressed;

        public void Initialize()
        {
            SubscribeOnGameplayEvents();
            SubscribeOnAnyKeyEvents();
            SubscribeOnUiEvents();

            _controls.Gameplay.Enable();
        }

        public void Dispose()
        {
            UnsubscribeOnGameplayEvents();
            UnsubscribeOnAnyKeyEvents();
            UnsubscribeFromUiEvents();

            _isDashPressed?.Dispose();
            _pausePressed?.Dispose();
            _compositeDisposable?.Dispose();
            _controls?.Dispose();
        }

        public void SwitchToUiInput()
        {
            _controls.Gameplay.Disable();
            _controls.AnyKey.Disable();
            _controls.UI.Enable();
        }

        public void SwitchToGameInput()
        {
            _controls.UI.Disable();
            _controls.AnyKey.Disable();
            _controls.Gameplay.Enable();
        }

        public void SwitchToAnyKeyInput()
        {
            _controls.UI.Disable();
            _controls.Gameplay.Disable();
            _controls.AnyKey.Enable();
        }

        #region Gameplay

        private void SubscribeOnGameplayEvents()
        {
            _controls.Gameplay.Attack.performed += OnAttackPerformed;
            _controls.Gameplay.Dash.started += OnDashStarted;
            _controls.Gameplay.Dash.canceled += OnDashCanceled;
            _controls.Gameplay.Pause.performed += OnPausePerformed;
            _controls.Gameplay.Restart.performed += OnRestartLevelPressed;
        }

        private void UnsubscribeOnGameplayEvents()
        {
            _controls.Gameplay.Attack.performed -= OnAttackPerformed;
            _controls.Gameplay.Dash.started -= OnDashStarted;
            _controls.Gameplay.Dash.canceled -= OnDashCanceled;
            _controls.Gameplay.Pause.performed -= OnPausePerformed;
            _controls.Gameplay.Restart.performed -= OnRestartLevelPressed;
        }

        private void OnPausePerformed(InputAction.CallbackContext obj)
        {
            _pausePressed.Execute();
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
        
        private void OnRestartLevelPressed(InputAction.CallbackContext obj)
        {
            _restartLevelPressed.Execute();
        }

        #endregion
 
        #region AnyKey
        
        private void SubscribeOnAnyKeyEvents()
        {
            _controls.AnyKey.AnyKey.performed += OnAnyKeyPerformed;
        }
        
        private void UnsubscribeOnAnyKeyEvents()
        {
            _controls.AnyKey.AnyKey.performed -= OnAnyKeyPerformed;
        }

        private void OnAnyKeyPerformed(InputAction.CallbackContext obj)
        {
            _anyKeyPressed.Execute();
        }

        #endregion
        
        #region AnyKey
        
        private void SubscribeOnUiEvents()
        {
            _controls.UI.CloseWindow.performed += OnCloseWindowPerformed;
        }

        private void UnsubscribeFromUiEvents()
        {
            _controls.UI.CloseWindow.performed -= OnCloseWindowPerformed;
        }

        private void OnCloseWindowPerformed(InputAction.CallbackContext obj)
        {
            _closeWindowPressed.Execute();
        }

        #endregion
    }
}