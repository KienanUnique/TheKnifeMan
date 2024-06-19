using Db.EnemiesParametersProvider.Parameters;
using Game.Enemy.Data;
using Game.Object.Part;
using Game.Utils;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy.Parts.Visual
{
    public class DefaultEnemyVisualPart : AObjectPart<AEnemyData>, IEnemyVisualPartBase
    {
        private readonly IEnemyParametersBase _parameters;

        private CompositeDisposable _aliveDisposables;

        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private Transform _transform;
        private SpriteRenderer _spriteRenderer;

        private bool _leftRotationForFlipX;

        public DefaultEnemyVisualPart(IEnemyParametersBase parameters)
        {
            _parameters = parameters;
        }

        public override void Initialize()
        {
            _animator = Data.Animator;
            _navMeshAgent = Data.NavMeshAgent;
            _transform = _navMeshAgent.transform;
            _spriteRenderer = Data.MainSprite;

            _leftRotationForFlipX = _spriteRenderer.flipX;
        }

        public override void Dispose()
        {
            _aliveDisposables?.Dispose();
        }

        private void HandleRotation()
        {
            var currentPosition = _transform.position;

            Vector3 lookAtPosition;
            if (_navMeshAgent.hasPath && _navMeshAgent.path.corners.Length > 1)
            {
                var nextPosition = _navMeshAgent.path.corners[1];
                lookAtPosition = nextPosition;
            }
            else
            {
                lookAtPosition = _navMeshAgent.destination;
            }

            var lookDirection = lookAtPosition - currentPosition;

            if (lookDirection.x > 0)
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
            var isMoving = _navMeshAgent.velocity.sqrMagnitude >= _parameters.AnimatorMovingVelocityThreshold;
            _animator.SetBool(AnimationKeys.IsMoving, isMoving);
        }
    }
}