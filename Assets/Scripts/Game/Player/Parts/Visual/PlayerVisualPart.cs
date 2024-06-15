using Db.Player;
using Game.Object.Part;
using Game.Utils;
using Services.Input;
using Services.ScreenPosition;
using UniRx;
using UnityEngine;

namespace Game.Player.Parts.Visual
{
    public class PlayerVisualPart : AObjectPart<PlayerData>, IPlayerVisualPart
    {
        private readonly IPlayerParameters _playerParameters;
        private readonly IScreenPositionService _screenPositionService;
        private readonly IInputService _inputService;
        
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;

        private Vector2 _lookDirection;

        public PlayerVisualPart(
            IPlayerParameters playerParameters, 
            IScreenPositionService screenPositionService, 
            IInputService inputService
        )
        {
            _playerParameters = playerParameters;
            _screenPositionService = screenPositionService;
            _inputService = inputService;
        }

        public override void Initialize()
        {
            _animator = Data.Animator;
            _rigidbody = Data.MainRigidbody;
            _spriteRenderer = Data.MainSprite;

            Observable.EveryUpdate().Subscribe(_ => OnUpdate()).AddTo(_compositeDisposable);
        }
        
        public override void Dispose()
        {
            _compositeDisposable.Dispose();
        }

        private void OnUpdate()
        {
            HandleMoving();
            HandleRotation();
        }

        private void HandleMoving()
        {
            var isMoving = _rigidbody.velocity.sqrMagnitude >= _playerParameters.AnimatorMovingVelocityThreshold;
            _animator.SetBool(AnimationKeys.IsMoving, isMoving);
        }

        private void HandleRotation()
        {
            var mouseScreenPosition = _inputService.MousePosition;
            var mouseWorldPosition = _screenPositionService.ConvertScreenPositionToWorld(mouseScreenPosition);
            var thisPosition = _rigidbody.position;
            
            _lookDirection = (mouseWorldPosition - thisPosition).normalized;

            var needRotateRight = _lookDirection.x > 0;
            _spriteRenderer.flipX = needRotateRight;
        }
    }
}