using Game.Enemy.Data;
using Game.Object.Part;
using Game.Projectile;
using Game.Projectile.Factory;
using Game.Projectile.Pattern;
using Game.Projectile.TypeData;
using UnityEngine;

namespace Game.Enemy.Parts.Attacker.Impl
{
    public class EnemyProjectileAttacker : AObjectPart<IProjectileEnemyData>, IEnemyProjectileAttacker
    {
        private readonly IProjectilesFactory _projectilesFactory;
        private IProjectilesSender _sender;

        public EnemyProjectileAttacker(IProjectilesFactory projectilesFactory)
        {
            _projectilesFactory = projectilesFactory;
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
        }

        public void DisableAndReset()
        {
        }

        public void AttackWithProjectile(IProjectilesPattern pattern, IProjectileType type, Vector2 attackDirection)
        {
            var rotation = Quaternion.FromToRotation(Vector2.left, attackDirection);
            var position = Data.ProjectilesSpawnPoint.position;
            _projectilesFactory.Create(pattern, type, _sender, position, rotation);
        }
    }
}