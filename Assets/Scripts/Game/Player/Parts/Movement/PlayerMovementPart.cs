using Db.Player;
using Game.Object.Part;
using Services.Input;
using UniRx;
using UnityEngine;

namespace Game.Player.Parts.Movement
{
    public class PlayerMovementPart : AObjectPart<PlayerView>, IPlayerMovementPart
    {
        private readonly IInputService _inputService;
        private readonly IPlayerParameters _playerParameters;
        
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private CompositeDisposable _movementDisposable;
        private Rigidbody2D _rigidbody;

        private bool _canMove = false;

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
            _rigidbody = View.MainRigidbody;
        }

        public override void Dispose()
        {
            _compositeDisposable.Dispose();
            _movementDisposable?.Dispose();
        }

        public void Enable()
        {
            if(_canMove)
                return;

            _canMove = true;
            
            _movementDisposable?.Dispose();
            _movementDisposable = new CompositeDisposable();

            Observable.EveryUpdate().Subscribe(_ => OnUpdate()).AddTo(_movementDisposable);
        }

        private void OnUpdate()
        {
            var needDirection = _inputService.NeedDirection;
            var needSpeed = _playerParameters.MovementSpeed * needDirection;
            _rigidbody.velocity = needSpeed;
            
            Debug.Log($"_rigidbody.velocity: {_rigidbody.velocity.sqrMagnitude}");
        }

        public void Disable()
        {
            if(!_canMove)
                return;
            
            _canMove = false;
        }
    }
}