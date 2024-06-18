using Db.Player;
using Game.Object.Part;
using Game.Utils;
using Game.Utils.Directions;
using UniRx;
using UnityEngine;

namespace Game.Player.Parts.Visual
{
    public class PlayerVisualPart : AObjectPart<PlayerData>, IPlayerVisualPart
    {
        private readonly IPlayerParameters _playerParameters;

        private CompositeDisposable _aliveDisposables = new();

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;

        private Vector2 _lookDirection;

        public PlayerVisualPart(IPlayerParameters playerParameters)
        {
            _playerParameters = playerParameters;
        }

        public override void Initialize()
        {
            _animator = Data.Animator;
            _rigidbody = Data.MainRigidbody;
            _spriteRenderer = Data.MainSprite;

            Observable.EveryUpdate().Subscribe(_ => OnUpdate()).AddTo(_aliveDisposables);
        }

        public override void Dispose()
        {
            _aliveDisposables?.Dispose();
        }

        public void ChangeLookDirection(EDirection1D direction1D)
        {
            var needRotateRight = direction1D == EDirection1D.Right;
            _spriteRenderer.flipX = needRotateRight;
        }

        private void OnUpdate()
        {
            var isMoving = _rigidbody.velocity.sqrMagnitude >= _playerParameters.AnimatorMovingVelocityThreshold;
            _animator.SetBool(AnimationKeys.IsMoving, isMoving);
        }

        public void PlayInjuredAnimation()
        {
            _animator.SetBool(AnimationKeys.IsInjured, true);
        }

        public void PlayDeathAnimation()
        {
            _aliveDisposables?.Dispose();
            _aliveDisposables = null;

            _animator.SetTrigger(AnimationKeys.Dead);
        }

        public void PlayAttackAnimation(EDirection2D direction2D)
        {
            _animator.SetInteger(AnimationKeys.AttackDirection, (int) direction2D);
            _animator.SetTrigger(AnimationKeys.AttackTrigger);
        }
    }
}