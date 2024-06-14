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
        
        public Vector2 NeedDirection { get; private set; }
        public IReactiveProperty<bool> IsDashPressed => _isDashPressed;
        public IObservable<Unit> PausePressed => _pausePressed;


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
            _controls.Gameplay.Movement.performed += OnMovementPerformed;
            
            _controls.Gameplay.Dash.started += OnDashStarted;
            _controls.Gameplay.Dash.canceled += OnDashCanceled;
        }
        
        private void UnsubscribeOnGameplayEvents()
        {
            _controls.Gameplay.Movement.performed -= OnMovementPerformed;
            
            _controls.Gameplay.Dash.started -= OnDashStarted;
            _controls.Gameplay.Dash.canceled -= OnDashCanceled;
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            NeedDirection = obj.ReadValue<Vector2>();
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