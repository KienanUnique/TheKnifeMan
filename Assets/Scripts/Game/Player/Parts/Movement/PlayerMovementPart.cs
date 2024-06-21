using System;
using Db.Player;
using Game.Object.Part;
using Services.Input;
using UniRx;
using UnityEngine;

namespace Game.Player.Parts.Movement
{
    public class PlayerMovementPart : AObjectPart<PlayerData>, IPlayerMovementPart
    {
        private readonly IInputService _inputService;
        private readonly IPlayerParameters _playerParameters;
        
        private readonly ReactiveCommand _dashStarted = new();

        private CompositeDisposable _movementDisposable;
        private Rigidbody2D _rigidbody;

        private bool _isMovementDisabled = false;
        private bool _canDash = false;
        private bool _canWalk = false;

        public IObservable<Unit> DashStarted => _dashStarted;

        private bool IsMoveDirectionNotInputted => _inputService.NeedDirection == Vector2.zero;

        public PlayerMovementPart(
            IInputService inputService,
            IPlayerParameters playerParameters
        )
        {
            _inputService = inputService;
            _playerParameters = playerParameters;
        }

        public override void Initialize()
        {
            _rigidbody = Data.MainRigidbody;
        }

        public override void Dispose()
        {
            _movementDisposable?.Dispose();
        }

        public void Enable()
        {
            if (_isMovementDisabled)
                return;

            _isMovementDisabled = true;
            _canDash = true;
            _canWalk = true;
                
            _rigidbody.isKinematic = false;

            _movementDisposable?.Dispose();
            _movementDisposable = new CompositeDisposable();

            Observable.EveryUpdate().Subscribe(_ => OnUpdate()).AddTo(_movementDisposable);
            _inputService.IsDashPressed.Subscribe(OnDashPressed).AddTo(_movementDisposable);
        }

        public void Disable()
        {
            if (!_isMovementDisabled)
                return;

            _isMovementDisabled = false;
            _movementDisposable?.Dispose();
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.isKinematic = true;
        }

        private void OnUpdate()
        {
            if (_canDash && _inputService.IsDashPressed.Value && !IsMoveDirectionNotInputted)
            {
                Dash();
            }
            else if (_canWalk)
            {
                var needDirection = _inputService.NeedDirection;
                var needSpeed = _playerParameters.MovementSpeed * needDirection;
                _rigidbody.velocity = needSpeed;
            }
        }

        private void OnDashPressed(bool isDashPressed)
        {
            if(!_canDash || !isDashPressed || IsMoveDirectionNotInputted)
                return;
            
            Dash();
        }

        private void Dash()
        {
            var needDirection = _inputService.NeedDirection;

            var dashForce = needDirection * _playerParameters.DashForce;

            var oldDrag = _rigidbody.drag;
            _rigidbody.drag = _playerParameters.DashDrag;
            
            _rigidbody.AddForce(dashForce, ForceMode2D.Impulse);

            _canWalk = false;
            Observable.Timer(TimeSpan.FromSeconds(_playerParameters.DashDurationSeconds))
                .Subscribe(_ =>
                {
                    _canWalk = true;
                    _rigidbody.drag = oldDrag;
                    
                    Observable.Timer(TimeSpan.FromSeconds(_playerParameters.DashCooldownSeconds))
                        .Subscribe(_ => _canDash = true).AddTo(_movementDisposable);
                    
                }).AddTo(_movementDisposable);

            _canDash = false;

            _dashStarted.Execute();
        }
    }
}