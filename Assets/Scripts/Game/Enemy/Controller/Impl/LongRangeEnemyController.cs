using Game.Character.Parts.AnimatorStatus;
using Game.Enemy.ActionsExecutor;
using Game.Enemy.Data.Impl;
using Game.Enemy.Parts.Attacker;
using Game.Enemy.Parts.Character;
using Game.Enemy.Parts.LookDirection;
using Game.Enemy.Parts.Visual;
using Game.Projectile;
using Game.Projectile.Pattern;
using Game.Projectile.TypeData;
using UniRx;
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
        public bool IsInReload => _projectileAttacker.IsInReload;

        public override void HandleEnable()
        {
            base.HandleEnable();
            _attackDirectionPart.AttackDirection.Subscribe(OnAttackDirection).AddTo(AliveDisposables);
        }

        public void AttackWithProjectile(IProjectilesPattern pattern, IProjectileType type)
        {
            var attackDirection = _attackDirectionPart.AttackDirection.Value;
            _projectileAttacker.AttackWithProjectile(pattern, type, attackDirection);
            _visualPart.PlayAttackAnimation(attackDirection);
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
        
        private void OnAttackDirection(Vector2 attackDirection)
        {
            _visualPart.RotateHandsTowardsAttackDirection(attackDirection);
        }
    }
}