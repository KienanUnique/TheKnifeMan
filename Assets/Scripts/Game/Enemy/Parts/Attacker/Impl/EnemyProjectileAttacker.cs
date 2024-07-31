using System;
using Db.EnemiesParameters.Parameters;
using Game.Enemy.Data;
using Game.Object.Part;
using Game.Projectile;
using Game.Projectile.Factory;
using Game.Utils.Directions;
using UniRx;
using UnityEngine;

namespace Game.Enemy.Parts.Attacker.Impl
{
    public class EnemyProjectileAttacker : AObjectPart<IProjectileEnemyData>, IEnemyProjectileAttacker
    {
        private readonly IProjectilesFactory _projectilesFactory;
        private readonly IProjectileEnemyParameters _projectileEnemyParameters;

        private CompositeDisposable _aliveDisposable;
        private IProjectilesSender _sender;

        private int _leftCountOfShoots;

        public bool IsCanShoot { get; private set; }

        public EnemyProjectileAttacker(
            IProjectilesFactory projectilesFactory,
            IProjectileEnemyParameters projectileEnemyParameters
        )
        {
            _projectilesFactory = projectilesFactory;
            _projectileEnemyParameters = projectileEnemyParameters;
        }

        public override void Initialize()
        {
            _sender = Data.RootTransform.GetComponent<IProjectilesSender>();
        }

        public override void Dispose()
        {
        }

        public void Enable()
        {
            _aliveDisposable = new CompositeDisposable();
            _leftCountOfShoots = _projectileEnemyParameters.CountOfAttacksInClip;
            IsCanShoot = true;
        }

        public void DisableAndReset()
        {
            _aliveDisposable?.Dispose();
        }

        public void AttackWithProjectile(Vector2 direction, EDirection1D direction1D)
        {
            var rotation = Quaternion.FromToRotation(Vector2.left, direction);

            var position = direction1D switch
            {
                EDirection1D.Left => Data.ProjectilesSpawnPointLeft.position,
                EDirection1D.Right => Data.ProjectilesSpawnPointRight.position,
                _ => throw new ArgumentOutOfRangeException(nameof(direction1D), direction1D, null)
            };

            _projectilesFactory.Create(_projectileEnemyParameters.Pattern, _projectileEnemyParameters.Type, _sender,
                position, rotation);

            _leftCountOfShoots--;
            
            IsCanShoot = false;

            if (_leftCountOfShoots <= 0)
            {
                var reloadDurationSeconds = _projectileEnemyParameters.ReloadDurationSeconds;
                Observable.Timer(TimeSpan.FromSeconds(reloadDurationSeconds)).Subscribe(_ =>
                    {
                        IsCanShoot = true;
                        _leftCountOfShoots = _projectileEnemyParameters.CountOfAttacksInClip;
                    })
                    .AddTo(_aliveDisposable);
            }
            else
            {
                var delayBetweenAttacks = _projectileEnemyParameters.DelayBetweenAttacks;
                Observable.Timer(TimeSpan.FromSeconds(delayBetweenAttacks)).Subscribe(_ => IsCanShoot = true)
                    .AddTo(_aliveDisposable);
            }
        }
    }
}