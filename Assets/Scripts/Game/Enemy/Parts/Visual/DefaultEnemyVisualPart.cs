using Db.EnemiesParametersProvider.Parameters;
using Game.Enemy.Data;
using Game.Object.Part;
using Game.Utils;
using UniRx;
using UnityEngine;

namespace Game.Enemy.Parts.Visual
{
    public class DefaultEnemyVisualPart : AObjectPart<AEnemyData>, IEnemyVisualPartBase
    {
        private readonly IEnemyParametersBase _parameters;

        private CompositeDisposable _aliveDisposables;

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;

        private bool _leftRotationForFlipX;


        public DefaultEnemyVisualPart(IEnemyParametersBase parameters)
        {
            _parameters = parameters;
        }

        public override void Initialize()
        {
            _animator = Data.Animator;
            _rigidbody = Data.MainRigidbody;
            _spriteRenderer = Data.MainSprite;

            _leftRotationForFlipX = _spriteRenderer.flipX;
        }

        public override void Dispose()
        {
            _aliveDisposables?.Dispose();
        }

        private void HandleRotation()
        {
            if (_rigidbody.velocity.x > 0)
                _spriteRenderer.flipX = !_leftRotationForFlipX;
            else
                _spriteRenderer.flipX = _leftRotationForFlipX;
        }

        public void PlayDeathAnimation()
        {
            _aliveDisposables?.Dispose();
            _aliveDisposables = null;

            _animator.SetTrigger(AnimationKeys.Dead);
        }

        public void Enable()
        {
            _aliveDisposables?.Dispose();
            _aliveDisposables = new CompositeDisposable();
            Observable.EveryUpdate().Subscribe(_ => OnUpdate()).AddTo(_aliveDisposables);
        }

        public void DisableAndReset()
        {
            _aliveDisposables?.Dispose();
            _animator.Rebind();
        }

        private void OnUpdate()
        {
            HandleMoving();
            HandleRotation();
        }

        private void HandleMoving()
        {
            var isMoving = _rigidbody.velocity.sqrMagnitude >= _parameters.AnimatorMovingVelocityThreshold;
            _animator.SetBool(AnimationKeys.IsMoving, isMoving);
        }
    }
}