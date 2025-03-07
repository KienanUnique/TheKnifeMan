﻿using Game.Character.Parts.AnimatorStatus;
using Game.Enemy.ActionsExecutor;
using Game.Enemy.Data.Impl;
using Game.Enemy.Parts.Attacker;
using Game.Enemy.Parts.Character;
using Game.Enemy.Parts.LookDirection;
using Game.Enemy.Parts.Visual;
using Game.Utils.Directions;
using UniRx;
using UnityEngine;
using Utils.Sounds;

namespace Game.Enemy.Controller.Impl
{
    public class MeleeEnemyController : AEnemyController<MeleeEnemyData>, IMeleeAttackEnemy
    {
        [SerializeField] private MeleeEnemyData data;

        private IEnemyCharacterPartBase _characterPart;
        private IMeleeEnemyVisualPart _visualPart;
        private IAnimatorStatusCheckerPart _animatorStatusCheckerPart;
        private IEnemyLookDirectionPart _lookDirectionPart;
        private IEnemyMeleeAttacker _attackerPart;

        private EDirection2D _attackDirection;
        private bool _isCanMeleeAttack;

        public bool IsCanMeleeAttack => _attackerPart.IsCanMeleeAttack;
        protected override MeleeEnemyData Data => data;
        protected override IEnemyCharacterPartBase CharacterPart => _characterPart;
        protected override IEnemyVisualPartBase EnemyVisualPart => _visualPart;
        protected override IAnimatorStatusCheckerPart AnimatorStatusCheckerPart => _animatorStatusCheckerPart;
        protected override IEnemyLookDirectionPart LookDirectionPart => _lookDirectionPart;

        public override void HandleEnable(Vector3 position)
        {
            base.HandleEnable(position);
            Data.AttackTrigger.AttackFramePlayed.Subscribe(_ => OnAttackFramePlayed()).AddTo(AliveDisposables);
        }

        public void AttackMelee()
        {
            if (_animatorStatusCheckerPart.IsAnimatorBusy)
                return;

            _attackDirection = _lookDirectionPart.CalculateLookDirection2D();
            _visualPart.PlayAttackAnimation(_attackDirection);
            GameSoundFxService.Play(EGameSoundFxType.EnemyMeleeAttack, transform);
        }

        protected override void ResolveParts()
        {
            _attackerPart = Resolve<IEnemyMeleeAttacker>();
            _characterPart = Resolve<IEnemyCharacterPartBase>();
            _visualPart = Resolve<IMeleeEnemyVisualPart>();
            _animatorStatusCheckerPart = Resolve<IAnimatorStatusCheckerPart>();
            _lookDirectionPart = Resolve<IEnemyLookDirectionPart>();
        }

        private void OnAttackFramePlayed()
        {
            _attackerPart.DamageTargets(_attackDirection);
        }
    }
}