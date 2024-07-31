using Game.Character.Parts.AnimatorStatus;
using Game.Enemy.ActionsExecutor;
using Game.Enemy.Data.Impl;
using Game.Enemy.Parts.Attacker;
using Game.Enemy.Parts.Character;
using Game.Enemy.Parts.LookDirection;
using Game.Enemy.Parts.Visual;
using Game.Projectile;
using Game.Utils.Directions;
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
        private Vector2 _attackDirection;
        private EDirection1D _attackDirection1D;

        protected override LongRangeEnemyData Data => data;
        protected override IEnemyCharacterPartBase CharacterPart => _characterPart;
        protected override IEnemyVisualPartBase EnemyVisualPart => _visualPart;
        protected override IAnimatorStatusCheckerPart AnimatorStatusCheckerPart => _animatorStatusCheckerPart;
        protected override IEnemyLookDirectionPart LookDirectionPart => _lookDirectionPart;

        public int InstanceId => GetInstanceID();
        public bool IsCanShoot => _projectileAttacker.IsCanShoot;

        public override void HandleEnable(Vector3 position)
        {
            base.HandleEnable(position);
            Data.AttackTrigger.AttackFramePlayed.Subscribe(_ => OnAttackFramePlayed()).AddTo(AliveDisposables);
        }

        private void OnAttackFramePlayed()
        {
            _projectileAttacker.AttackWithProjectile(_attackDirection, _attackDirection1D);
            GameSoundFxService.Play(EGameSoundFxType.EnemyShoot, transform);
        }

        public void AttackWithProjectile()
        {
            (_attackDirection, _attackDirection1D) = _attackDirectionPart.CalculateAttackDirection1D();

            _visualPart.PlayAttackAnimation(_attackDirection1D);
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