using System;
using Db.EnemiesParameters.Parameters;
using Game.Enemy.Data;
using Game.Object.Part;
using Game.Projectile;
using Game.Projectile.Factory;
using Game.Projectile.Pattern;
using Game.Projectile.TypeData;
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

        public bool IsInReload { get; private set; } = false;

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
        }

        public void DisableAndReset()
        {
            _aliveDisposable?.Dispose();
        }

        public void AttackWithProjectile(IProjectilesPattern pattern, IProjectileType type, Vector2 attackDirection)
        {
            var rotation = Quaternion.FromToRotation(Vector2.left, attackDirection);
            var position = Data.ProjectilesSpawnPoint.position;
            _projectilesFactory.Create(pattern, type, _sender, position, rotation);

            IsInReload = true;
            
            var reloadDurationSeconds = _projectileEnemyParameters.ReloadDurationSeconds;
            Observable.Timer(TimeSpan.FromSeconds(reloadDurationSeconds)).Subscribe(_ => IsInReload = false)
                .AddTo(_aliveDisposable);
        }
    }
}