using System;
using Db.Player;
using Game.Character.Parts.AnimatorStatus;
using Game.Interfaces;
using Game.Object;
using Game.Player.Parts.Attack;
using Game.Player.Parts.Character;
using Game.Player.Parts.LookDirection;
using Game.Player.Parts.Movement;
using Game.Player.Parts.Visual;
using Game.Utils;
using Game.Utils.Directions;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerController : AObjectController<PlayerData>, IPlayerInformation, IDamageable, INeedWaitInitializable, IGameStateListener
    {
        private readonly CompositeDisposable _aliveDisposable = new();
        private readonly ReactiveProperty<bool> _isInitilized = new();

        [SerializeField] private PlayerData data;

        [Inject] private IPlayerParameters _parameters;

        private IPlayerMovementPart _movementPart;
        private IPlayerVisualPart _visualPart;
        private IPlayerCharacterPart _characterPart;
        private IPlayerAttackPart _attackPart;
        private IPlayerLookDirectionPart _lookDirectionPart;
        private IAnimatorStatusCheckerPart _animatorStatusCheckerPart;

        private EDirection2D _attackDirection;

        public IReactiveProperty<bool> IsInitilized => _isInitilized;
        public Transform Transform => transform;
        public IReactiveProperty<int> Health => _characterPart.Health;
        public IReactiveProperty<bool> IsDead => _characterPart.IsDead;

        protected override PlayerData Data => data;

        public void HandleDamage(int damage)
        {
            _characterPart.HandleDamage(damage);
        }
        
        public void OnGameEnd()
        {
            _movementPart.Disable();
            _aliveDisposable?.Dispose();
        }

        protected override void ResolveParts()
        {
            _movementPart = Resolve<IPlayerMovementPart>();
            _visualPart = Resolve<IPlayerVisualPart>();
            _characterPart = Resolve<IPlayerCharacterPart>();
            _attackPart = Resolve<IPlayerAttackPart>();
            _lookDirectionPart = Resolve<IPlayerLookDirectionPart>();
            _animatorStatusCheckerPart = Resolve<IAnimatorStatusCheckerPart>();
        }

        protected override void HandleInitialize()
        {
            _characterPart.IsDead.Subscribe(OnDead).AddTo(_aliveDisposable);
            _characterPart.Health.Subscribe(OnHealth).AddTo(_aliveDisposable);

            _attackPart.Attack.Subscribe(_ => OnAttack()).AddTo(_aliveDisposable);
            Data.AttackTrigger.AttackFramePlayed.Subscribe(_ => OnAttackFramePlayed()).AddTo(_aliveDisposable);

            _lookDirectionPart.LookDirection1D.Subscribe(OnLookDirection).AddTo(_aliveDisposable);

            _animatorStatusCheckerPart.IsAnimatorBusyChanged.Subscribe(OnIsAnimatorBusy).AddTo(_aliveDisposable);
            
            _movementPart.DashStarted.Subscribe(_ => OnDashStarted()).AddTo(_aliveDisposable);

            _movementPart.Enable();
            _visualPart.ChangeLookDirection(_lookDirectionPart.LookDirection1D.Value);

            CompositeDisposable.Add(_aliveDisposable);

            _isInitilized.Value = true;
        }

        private void OnDashStarted()
        {
            _visualPart.StartPlayingDashAnimation();
        }

        private void OnIsAnimatorBusy(bool isBusy)
        {
            if (isBusy)
                return;

            var lookDirection = _lookDirectionPart.LookDirection1D.Value;
            _visualPart.ChangeLookDirection(lookDirection);
        }

        private void OnLookDirection(EDirection1D newDirection)
        {
            if (_animatorStatusCheckerPart.IsAnimatorBusy)
                return;

            _visualPart.ChangeLookDirection(newDirection);
        }

        private void OnAttackFramePlayed()
        {
            _attackPart.DamageTargets(_attackDirection);
        }

        private void OnAttack()
        {
            if (_animatorStatusCheckerPart.IsAnimatorBusy)
                return;

            _attackDirection = _lookDirectionPart.CalculateLookDirection2D();
            _visualPart.PlayAttackAnimation(_attackDirection);
        }

        private void OnHealth(int health)
        {
            if (_characterPart.IsDead.Value)
                return;

            var lowHealthThreshold = _parameters.LowHealthThreshold;
            if (health > lowHealthThreshold) return;

            _visualPart.PlayInjuredAnimation();
        }

        private void OnDead(bool isDead)
        {
            if (!isDead)
                return;

            _visualPart.PlayDeathAnimation();
            _movementPart.Disable();

            _aliveDisposable?.Dispose();
        }
    }
}