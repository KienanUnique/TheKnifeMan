using System;
using Db.EnemiesParametersProvider.Parameters;
using Game.Enemy.Data;
using Game.Object.Part;
using Game.Utils;
using Game.Utils.Directions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy.Parts.Visual
{
    public class AEnemyVisualPart : AObjectPart<AEnemyData>
    {
        protected readonly IEnemyParametersBase Parameters;

        private CompositeDisposable _aliveDisposables;

        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private SpriteRenderer _spriteRenderer;

        private bool _leftRotationForFlipX;

        protected Animator Animator => _animator;
        protected NavMeshAgent NavMeshAgent => _navMeshAgent;
        protected SpriteRenderer SpriteRenderer => _spriteRenderer;
        protected CompositeDisposable AliveDisposables => _aliveDisposables;

        protected AEnemyVisualPart(IEnemyParametersBase parameters)
        {
            Parameters = parameters;
        }

        public override void Initialize()
        {
            _animator = Data.Animator;
            _navMeshAgent = Data.NavMeshAgent;
            _spriteRenderer = Data.MainSprite;

            _leftRotationForFlipX = _spriteRenderer.flipX;
        }

        public override void Dispose()
        {
            _aliveDisposables?.Dispose();
        }

        public void PlayDeathAnimation()
        {
            _aliveDisposables?.Dispose();
            _aliveDisposables = null;

            _animator.SetTrigger(AnimationKeys.Dead);
        }

        public void ChangeLookDirection(EDirection1D direction1D)
        {
            _spriteRenderer.flipX = direction1D switch
            {
                EDirection1D.Left => _leftRotationForFlipX,
                EDirection1D.Right => !_leftRotationForFlipX,
                _ => throw new ArgumentOutOfRangeException(nameof(direction1D), direction1D, null)
            };
        }

        public void Enable()
        {
            _aliveDisposables?.Dispose();
            _aliveDisposables = new CompositeDisposable();
            Observable.EveryUpdate().Subscribe(_ => UpdateIsMovingState()).AddTo(_aliveDisposables);
        }

        public void DisableAndReset()
        {
            _aliveDisposables?.Dispose();
            _animator.Rebind();
        }

        protected virtual void UpdateIsMovingState()
        {
            var isMoving = _navMeshAgent.velocity.sqrMagnitude >= Parameters.AnimatorMovingVelocityThreshold;
            _animator.SetBool(AnimationKeys.IsMoving, isMoving);
        }
    }
}