using System;
using System.Collections.Generic;
using Db.LayerMasks;
using Db.Player;
using Game.Interfaces;
using Game.Object.Part;
using Game.Utils.Directions;
using Services.Input;
using UniRx;
using UnityEngine;

namespace Game.Player.Parts.Attack.Impl
{
    public class PlayerAttackPart : AObjectPart<PlayerData>, IPlayerAttackPart
    {
        private const int MaxOverlapCount = 15;

        private readonly IInputService _inputService;
        private readonly IPlayerParameters _playerParameters;
        private readonly ILayerMasksParameters _layerMasksParameters;

        private readonly ReactiveCommand _onAttack = new();
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly Collider2D[] _overlapResult = new Collider2D[MaxOverlapCount];

        public IObservable<Unit> Attack => _onAttack;

        public PlayerAttackPart(
            IInputService inputService,
            IPlayerParameters playerParameters,
            ILayerMasksParameters layerMasksParameters
        )
        {
            _inputService = inputService;
            _playerParameters = playerParameters;
            _layerMasksParameters = layerMasksParameters;
        }

        public override void Initialize()
        {
            _inputService.AttackPressed.Subscribe(_ => OnAttackPressed()).AddTo(_compositeDisposable);
        }

        public override void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public void DamageTargets(EDirection2D attackDirection)
        {
            var needCollider = attackDirection switch
            {
                EDirection2D.Up => Data.DamageColliderUp,
                EDirection2D.Down => Data.DamageColliderDown,
                EDirection2D.Left => Data.DamageColliderLeft,
                EDirection2D.Right => Data.DamageColliderRight,
                _ => throw new ArgumentOutOfRangeException(nameof(attackDirection), attackDirection, null)
            };

            var position = (Vector2) needCollider.transform.position + needCollider.offset;
            var countOfColliders = Physics2D.OverlapBoxNonAlloc(position, needCollider.size, 0f, _overlapResult,
                _layerMasksParameters.EnemyLayer);

            var foundedTargets = new HashSet<IDamageable>();
            for (var i = 0; i < countOfColliders; i++)
            {
                var collider = _overlapResult[i];

                if (!collider.TryGetComponent(out IDamageable damageable))
                    continue;

                foundedTargets.Add(damageable);
            }

            var damage = _playerParameters.Damage;
            foreach (var target in foundedTargets) target.HandleDamage(damage);
        }

        private void OnAttackPressed()
        {
            _onAttack.Execute();
        }
    }
}