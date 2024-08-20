using System;
using Db.Player;
using DG.Tweening;
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

        private Color _originalSpriteColor;
        private Sequence _blinkAnimationSequence;

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
            
            _originalSpriteColor = Data.MainSprite.color;

            SetupBlinkAnimation();
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

        public void PlayBlinkAnimation(float durationSeconds) => _blinkAnimationSequence.Restart();

        private void StopPlayingDashAnimation()
        {
            _isInDash = false;
            _animator.SetBool(AnimationKeys.IsDashing, false);
        }
        
        private void SetupBlinkAnimation()
        {
            var blinksAnimationDuration = _playerParameters.AfterDamageImmortalDurationSeconds;
            var totalBlinksCount = _playerParameters.AfterDamageBlinksCount * 2;
            var delayBetweenBlinks = blinksAnimationDuration / totalBlinksCount;

            _blinkAnimationSequence = DOTween.Sequence();
            for (var i = 0; i < _playerParameters.AfterDamageBlinksCount; i++)
            {
                _blinkAnimationSequence.AppendCallback(SetMainSpriteBlinkColor);
                _blinkAnimationSequence.AppendInterval(delayBetweenBlinks);
                _blinkAnimationSequence.AppendCallback(ResetMainSpriteColor);
                _blinkAnimationSequence.AppendInterval(delayBetweenBlinks);
            }

            _blinkAnimationSequence.SetAutoKill(false);
            _blinkAnimationSequence.SetLink(Data.RootTransform.gameObject);
            _blinkAnimationSequence.Pause();
        }
        
        private void SetMainSpriteBlinkColor()
        {
            SetMainSpriteColor(_playerParameters.AfterDamageBlinkColor);
        }
        
        private void ResetMainSpriteColor()
        {
            SetMainSpriteColor(_originalSpriteColor);
        }
        
        private void SetMainSpriteColor(Color newColor)
        {
            Data.MainSprite.color = newColor;
        }
    }
}