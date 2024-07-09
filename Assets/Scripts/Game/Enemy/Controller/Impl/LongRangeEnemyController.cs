using Game.Character.Parts.AnimatorStatus;
using Game.Enemy.ActionsExecutor;
using Game.Enemy.Data.Impl;
using Game.Enemy.Parts.Attacker;
using Game.Enemy.Parts.Character;
using Game.Enemy.Parts.LookDirection;
using Game.Enemy.Parts.Visual;
using Game.Projectile;
using UnityEngine;
using Utils.Sounds;

namespace Game.Enemy.Controller.Impl
{
    public class LongRangeEnemyController : AEnemyController<LongRangeEnemyData>, IProjectileAttackEnemy
    {
        [SerializeField] private LongRangeEnemyData data;

        private IEnemyCharacterPartBase _characterPart;
        private IProjectileEnemyVisualPart _visualPart;
        private IAnimatorStatusCheckerPart _animatorStatusCheckerPart;
        private IProjectileEnemyAttackDirectionPart _attackDirectionPart;
        private IEnemyLookDirectionPart _lookDirectionPart;
        private IEnemyProjectileAttacker _projectileAttacker;

        protected override LongRangeEnemyData Data => data;
        protected override IEnemyCharacterPartBase CharacterPart => _characterPart;
        protected override IEnemyVisualPartBase EnemyVisualPart => _visualPart;
        protected override IAnimatorStatusCheckerPart AnimatorStatusCheckerPart => _animatorStatusCheckerPart;
        protected override IEnemyLookDirectionPart LookDirectionPart => _lookDirectionPart;

        public int InstanceId => GetInstanceID();
        public bool IsCanShoot => _projectileAttacker.IsCanShoot;

        public void AttackWithProjectile()
        {
            var (vectorDirection, concreteDirection) = _attackDirectionPart.CalculateAttackDirection();
            _projectileAttacker.AttackWithProjectile(vectorDirection);
            _visualPart.PlayAttackAnimation(concreteDirection);
            GameSoundFxService.Play(EGameSoundFxType.EnemyShoot, transform);
        }
        
        public bool Equals(IProjectilesSender other)
        {
            return other != null && InstanceId.Equals(other.InstanceId);
        }

        protected override void ResolveParts()
        {
            _characterPart = Resolve<IEnemyCharacterPartBase>();
            _visualPart = Resolve<IProjectileEnemyVisualPart>();
            _animatorStatusCheckerPart = Resolve<IAnimatorStatusCheckerPart>();
            _lookDirectionPart = Resolve<IEnemyLookDirectionPart>();
            _attackDirectionPart = Resolve<IProjectileEnemyAttackDirectionPart>();
            _projectileAttacker = Resolve<IEnemyProjectileAttacker>();
        }
    }
}