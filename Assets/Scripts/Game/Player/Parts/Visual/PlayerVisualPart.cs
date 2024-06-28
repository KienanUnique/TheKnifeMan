using System;
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
        private bool _isInDash = false;
        private IDisposable _dashDisposable;

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
            _dashDisposable?.Dispose();
            _aliveDisposables?.Dispose();
        }

        public void ChangeLookDirection(EDirection1D direction1D)
        {
            var needRotateRight = direction1D == EDirection1D.Right;
            _spriteRenderer.flipX = needRotateRight;
        }

        private void OnUpdate()
        {
            if(_isInDash)
                return;
            
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

        public void StartPlayingDashAnimation()
        {
            _isInDash = true;
            _animator.SetBool(AnimationKeys.IsMoving, false);
            _animator.SetBool(AnimationKeys.IsDashing, true);
            
            var endAnimationInvokeDelay = _playerParameters.DashDurationSeconds - _playerParameters.DashEndAnimationDurationSeconds;
            endAnimationInvokeDelay = Mathf.Clamp(endAnimationInvokeDelay, 0f, endAnimationInvokeDelay);
            
            _dashDisposable = Observable.Timer(TimeSpan.FromSeconds(endAnimationInvokeDelay))
                .Subscribe(_ => StopPlayingDashAnimation());
        }

        private void StopPlayingDashAnimation()
        {
            _isInDash = false;
            _animator.SetBool(AnimationKeys.IsDashing, false);
        }
    }
}