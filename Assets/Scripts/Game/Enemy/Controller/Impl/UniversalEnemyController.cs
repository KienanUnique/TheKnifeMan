using Game.Character.Parts.AnimatorStatus;
using Game.Enemy.ActionsExecutor;
using Game.Enemy.Data.Impl;
using Game.Enemy.Parts.Attacker;
using Game.Enemy.Parts.Character;
using Game.Enemy.Parts.LookDirection;
using Game.Enemy.Parts.Visual;
using Game.Projectile;
using Game.Utils;
using Game.Utils.Directions;
using UniRx;
using UnityEngine;
using Utils.Sounds;

namespace Game.Enemy.Controller.Impl
{
    public class UniversalEnemyController : AEnemyController<UniversalEnemyData>, IMeleeAttackEnemy, IProjectileAttackEnemy
    {
        [SerializeField] private UniversalEnemyData data;
        
        private IEnemyCharacterPartBase _characterPart;
        private IUniversalEnemyVisualPart _visualPart;
        private IAnimatorStatusCheckerPart _animatorStatusCheckerPart;
        private IEnemyLookDirectionPart _lookDirectionPart;
        private IProjectileEnemyAttackDirectionPart _attackDirectionPart;
        private IEnemyMeleeAttacker _meleeAttackerPart;
        private IEnemyProjectileAttacker _projectileAttacker;
        
        private Vector2 _projectileAttackDirection;
        private EDirection1D _projectileAttackDirection1D;
        private EDirection2D _meleeAttackDirection;
        private EAttackType _currentAttackType;

        public bool IsCanMeleeAttack => _meleeAttackerPart.IsCanMeleeAttack;
        public int InstanceId => GetInstanceID();
        public bool IsCanShoot => _projectileAttacker.IsCanShoot;
        protected override UniversalEnemyData Data => data;
        protected override IEnemyCharacterPartBase CharacterPart => _characterPart;
        protected override IEnemyVisualPartBase EnemyVisualPart => _visualPart;
        protected override IAnimatorStatusCheckerPart AnimatorStatusCheckerPart => _animatorStatusCheckerPart;
        protected override IEnemyLookDirectionPart LookDirectionPart => _lookDirectionPart;

        public override void HandleEnable(Vector3 position)
        {
            base.HandleEnable(position);
            Data.AttackTrigger.AttackFramePlayed.Subscribe(_ => OnAttackFramePlayed()).AddTo(AliveDisposables);
        }
        
        private void OnAttackFramePlayed()
        {
            switch (_currentAttackType)
            {
                case EAttackType.Melee:
                    _meleeAttackerPart.DamageTargets(_meleeAttackDirection);
                    break;
                case EAttackType.LongRange:
                    _projectileAttacker.AttackWithProjectile(_projectileAttackDirection, _projectileAttackDirection1D);
                    GameSoundFxService.Play(EGameSoundFxType.EnemyShoot, transform);
                    break;
            }
            
            _currentAttackType = EAttackType.None;
        }

        public void AttackWithProjectile()
        {
            (_projectileAttackDirection, _projectileAttackDirection1D) = _attackDirectionPart.CalculateAttackDirection1D();

            _visualPart.PlayAttackAnimation(_projectileAttackDirection1D);
            
            _currentAttackType = EAttackType.LongRange;
        }
        
        public void AttackMelee()
        {
            if (_animatorStatusCheckerPart.IsAnimatorBusy)
                return;

            _meleeAttackDirection = _lookDirectionPart.CalculateLookDirection2D();
            _visualPart.PlayAttackAnimation(_meleeAttackDirection);
            GameSoundFxService.Play(EGameSoundFxType.EnemyMeleeAttack, transform);
            
            _currentAttackType = EAttackType.Melee;
        }

        public bool Equals(IProjectilesSender other)
        {
            return other != null && InstanceId.Equals(other.InstanceId);
        }

        protected override void ResolveParts()
        {
            _characterPart = Resolve<IEnemyCharacterPartBase>();
            _visualPart = Resolve<IUniversalEnemyVisualPart>();
            _animatorStatusCheckerPart = Resolve<IAnimatorStatusCheckerPart>();
            _lookDirectionPart = Resolve<IEnemyLookDirectionPart>();
            _attackDirectionPart = Resolve<IProjectileEnemyAttackDirectionPart>();
            _meleeAttackerPart = Resolve<IEnemyMeleeAttacker>();
            _projectileAttacker = Resolve<IEnemyProjectileAttacker>();
        }
    }
}