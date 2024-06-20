using Game.Character.Parts.AnimatorStatus;
using Game.Enemy.ActionsExecutor;
using Game.Enemy.Context;
using Game.Enemy.Context.Impl;
using Game.Enemy.Data.Impl;
using Game.Enemy.Parts.Attacker;
using Game.Enemy.Parts.Character;
using Game.Enemy.Parts.LookDirection;
using Game.Enemy.Parts.Visual;
using Game.Utils.Directions;
using UniRx;
using UnityEngine;

namespace Game.Enemy.Controller.Impl
{
    public class SimpleEnemyController : AEnemyController<SimpleEnemyData>, IMeleeAttackEnemy
    {
        [SerializeField] private SimpleEnemyData data;

        private IEnemyCharacterPartBase _characterPart;
        private IMeleeEnemyVisualPart _visualPart;
        private IAnimatorStatusCheckerPart _animatorStatusCheckerPart;
        private IEnemyLookDirectionPart _lookDirectionPart;
        private IEnemyMeleeAttacker _attacker;

        private EDirection2D _attackDirection;

        protected override SimpleEnemyData Data => data;
        protected override IEnemyCharacterPartBase CharacterPart => _characterPart;
        protected override IEnemyVisualPartBase EnemyVisualPart => _visualPart;
        protected override IAnimatorStatusCheckerPart AnimatorStatusCheckerPart => _animatorStatusCheckerPart;
        protected override IEnemyLookDirectionPart LookDirectionPart => _lookDirectionPart;


        public void AttackMelee()
        {
            if (_animatorStatusCheckerPart.IsAnimatorBusy)
                return;

            _attackDirection = _lookDirectionPart.CalculateLookDirection2D();
            _visualPart.PlayAttackAnimation(_attackDirection);
        }

        protected override void ResolveParts()
        {
            _attacker = Resolve<IEnemyMeleeAttacker>();
            _characterPart = Resolve<IEnemyCharacterPartBase>();
            _visualPart = Resolve<IMeleeEnemyVisualPart>();
            _animatorStatusCheckerPart = Resolve<IAnimatorStatusCheckerPart>();
            _lookDirectionPart = Resolve<IEnemyLookDirectionPart>();
        }

        public override void HandleEnable()
        {
            base.HandleEnable();
            Data.AttackTrigger.AttackFramePlayed.Subscribe(_ => OnAttackFramePlayed()).AddTo(AliveDisposables);
        }

        protected override IEnemyContextBase CreateContext()
        {
            return new DefaultEnemyContext(this, transform);
        }

        private void OnAttackFramePlayed()
        {
            _attacker.DamageTargets(_attackDirection);
        }
    }
}