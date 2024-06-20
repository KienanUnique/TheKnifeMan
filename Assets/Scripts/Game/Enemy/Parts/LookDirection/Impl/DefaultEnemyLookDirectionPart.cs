using Game.Enemy.Data;
using Game.Object.Part;
using Game.Utils.Directions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy.Parts.LookDirection.Impl
{
    public class DefaultEnemyLookDirectionPart : AObjectPart<AEnemyData>, IEnemyLookDirectionPart
    {
        private CompositeDisposable _aliveDisposable;
        private readonly ReactiveProperty<EDirection1D> _lookDirection1D = new();

        private NavMeshAgent _navMeshAgent;
        private Transform _transform;

        private Vector2 _lookDirection;

        public IReactiveProperty<EDirection1D> LookDirection1D => _lookDirection1D;

        public override void Initialize()
        {
            _navMeshAgent = Data.NavMeshAgent;
            _transform = Data.RootTransform;
        }

        public override void Dispose()
        {
            _aliveDisposable?.Dispose();
        }

        public void Enable()
        {
            _aliveDisposable?.Dispose();

            _aliveDisposable = new CompositeDisposable();
            Observable.EveryUpdate().Subscribe(_ => OnUpdate()).AddTo(_aliveDisposable);
        }

        public void DisableAndReset()
        {
            _aliveDisposable?.Dispose();
        }

        public EDirection2D CalculateLookDirection2D()
        {
            if (Mathf.Abs(_lookDirection.x) > Mathf.Abs(_lookDirection.y))
            {
                return _lookDirection.x > 0 ? EDirection2D.Right : EDirection2D.Left;
            }

            return _lookDirection.y > 0 ? EDirection2D.Up : EDirection2D.Down;
        }

        private void OnUpdate()
        {
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

            var currentPosition = _transform.position;
            _lookDirection = lookAtPosition - currentPosition;

            var needRotateRight = _lookDirection.x > 0;
            _lookDirection1D.Value = needRotateRight ? EDirection1D.Right : EDirection1D.Left;
        }
    }
}