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
using Game.Services.VFX;
using Game.Utils;
using Game.Utils.Directions;
using Services.Sound;
using UniRx;
using UnityEngine;
using Utils.Sounds;
using Zenject;

namespace Game.Player
{
    public class PlayerController : AObjectController<PlayerData>, IPlayerController, IDamageable, INeedWaitInitializable, IGameStateListener
    {
        private readonly CompositeDisposable _aliveDisposable = new();
        private readonly ReactiveProperty<bool> _isInitilized = new();

        [SerializeField] private PlayerData data;

        [Inject] private IPlayerParameters _parameters;
        [Inject] private IGameSoundFxService _gameSoundFxService;
        [Inject] private IVfxService _vfxService;

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
        public IObservable<Unit> EnemyDamaged => _attackPart.EnemyDamaged;

        protected override PlayerData Data => data;

        public void HandleDamage(int damage) => _characterPart.HandleDamage(damage);
        public void ResetHealth() => _characterPart.ResetHealth();

        public void OnGameEnd(bool isPlayerWin)
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
            _movementPart.DashEnded.Subscribe(_ => OnDashEnded()).AddTo(_aliveDisposable);

            _movementPart.Enable();
            _visualPart.ChangeLookDirection(_lookDirectionPart.LookDirection1D.Value);

            CompositeDisposable.Add(_aliveDisposable);

            _isInitilized.Value = true;
        }

        private void OnDashStarted()
        {
            _characterPart.EnableImmortal();
            _visualPart.StartPlayingDashAnimation();
            _gameSoundFxService.Play(EGameSoundFxType.PlayerDash, transform);
        }

        private void OnDashEnded()
        {
            _characterPart.DisableImmortal();
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
            _gameSoundFxService.Play(EGameSoundFxType.PlayerAttack, transform);
        }

        private void OnHealth(int health)
        {
            if (_characterPart.IsDead.Value)
                return;
            
            if(health == _parameters.Health)
                return;

            _vfxService.Play(EVfxType.DamageCharacter, transform.position);
            _gameSoundFxService.Play(EGameSoundFxType.PLayerDamageTaken, transform);
            
            var lowHealthThreshold = _parameters.LowHealthThreshold;
            if (health > lowHealthThreshold) return;

            _visualPart.PlayInjuredAnimation();
        }

        private void OnDead(bool isDead)
        {
            if (!isDead)
                return;

            _vfxService.Play(EVfxType.DamageCharacter, transform.position);
            _visualPart.PlayDeathAnimation();
            _movementPart.Disable();
            _gameSoundFxService.Play(EGameSoundFxType.PlayerDeath, transform);

            _aliveDisposable?.Dispose();
        }
    }
}