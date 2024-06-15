using Db.Player;
using Game.Object.Part;
using Game.Utils;
using UniRx;
using UnityEngine;

namespace Game.Player.Parts.Visual
{
    public class PlayerVisualPart : AObjectPart<PlayerView>, IPlayerVisualPart
    {
        private readonly IPlayerParameters _playerParameters;
        
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private Animator _animator;
        private Rigidbody2D _rigidbody;

        public PlayerVisualPart(IPlayerParameters playerParameters)
        {
            _playerParameters = playerParameters;
        }

        public override void Initialize()
        {
            _animator = View.Animator;
            _rigidbody = View.MainRigidbody;

            Observable.EveryUpdate().Subscribe(_ => UpdateMovement()).AddTo(_compositeDisposable);
        }

        private void UpdateMovement()
        {
            var isMoving = _rigidbody.velocity.sqrMagnitude >= _playerParameters.AnimatorMovingVelocityThreshold;
            _animator.SetBool(AnimationKeys.IsMoving, isMoving);
        }

        public override void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}