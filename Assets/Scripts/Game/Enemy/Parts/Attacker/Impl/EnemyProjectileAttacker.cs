using System;
using Db.EnemiesParameters.Parameters;
using Game.Enemy.Data;
using Game.Object.Part;
using Game.Projectile;
using Game.Projectile.Factory;
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

        private int leftCountOfShoots;

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
            leftCountOfShoots = _projectileEnemyParameters.CountOfAttacksInClip;
            IsCanShoot = true;
        }

        public void DisableAndReset()
        {
            _aliveDisposable?.Dispose();
        }

        public void AttackWithProjectile(Vector2 attackDirection)
        {
            var rotation = Quaternion.FromToRotation(Vector2.left, attackDirection);
            var position = Data.ProjectilesSpawnPoint.position;
            _projectilesFactory.Create(_projectileEnemyParameters.Pattern, _projectileEnemyParameters.Type, _sender,
                position, rotation);

            leftCountOfShoots--;
            
            IsCanShoot = false;

            if (leftCountOfShoots <= 0)
            {
                var reloadDurationSeconds = _projectileEnemyParameters.ReloadDurationSeconds;
                Observable.Timer(TimeSpan.FromSeconds(reloadDurationSeconds)).Subscribe(_ =>
                    {
                        IsCanShoot = true;
                        leftCountOfShoots = _projectileEnemyParameters.CountOfAttacksInClip;
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