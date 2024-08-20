using Game.Enemy.Data;
using Game.Object.Part;
using Game.Player;
using Game.Utils;
using Game.Utils.Directions;
using UniRx;
using UnityEngine;

namespace Game.Enemy.Parts.LookDirection.Impl
{
    public class ProjectileEnemyAttackDirectionPart : AObjectPart<IProjectileEnemyData>,
        IProjectileEnemyAttackDirectionPart
    {
        private readonly IPlayerInformation _playerInformation;

        private CompositeDisposable _aliveDisposable;

        public ProjectileEnemyAttackDirectionPart(IPlayerInformation playerInformation)
        {
            _playerInformation = playerInformation;
        }

        public override void Initialize()
        {
        }

        public override void Dispose()
        {
            _aliveDisposable?.Dispose();
        }

        public void Enable()
        {
            _aliveDisposable = new CompositeDisposable();
        }

        public void DisableAndReset()
        {
            _aliveDisposable?.Dispose();
        }

        public (Vector2, EDirection1D) CalculateAttackDirection1D()
        {
            var playerPosition = (Vector2) _playerInformation.Transform.position;
            var enemyPosition = (Vector2) Data.RootTransform.position;
            var direction = (playerPosition - enemyPosition).normalized;

            return (direction, direction.ToDirection1D());
        }
    }
}