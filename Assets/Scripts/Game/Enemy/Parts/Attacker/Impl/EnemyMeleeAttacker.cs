using System;
using System.Collections.Generic;
using Db.EnemiesParameters.Parameters;
using Db.LayerMasks;
using Game.Enemy.Data;
using Game.Interfaces;
using Game.Object.Part;
using Game.Utils.Directions;
using UniRx;
using UnityEngine;

namespace Game.Enemy.Parts.Attacker.Impl
{
    public class EnemyMeleeAttacker : AObjectPart<IMeleeEnemyData>, IEnemyMeleeAttacker
    {
        private const int MaxOverlapCount = 10;

        private readonly IMeleeEnemyParameters _parameters;
        private readonly ILayerMasksParameters _layerMasksParameters;

        private readonly Collider2D[] _overlapResult = new Collider2D[MaxOverlapCount];
        
        private int _leftCountOfAttacks;
        private CompositeDisposable _aliveDisposable;

        public bool IsCanMeleeAttack { get; private set; }

        public EnemyMeleeAttacker(
            IMeleeEnemyParameters parameters,
            ILayerMasksParameters layerMasksParameters
        )
        {
            _parameters = parameters;
            _layerMasksParameters = layerMasksParameters;
        }

        public override void Initialize()
        {
        }

        public override void Dispose()
        {
        }

        public void Enable()
        {
            _aliveDisposable = new CompositeDisposable();
            _leftCountOfAttacks = _parameters.CountOfAttacksInCombo;
            IsCanMeleeAttack = true;
        }

        public void DisableAndReset()
        {
            _aliveDisposable?.Dispose();
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
                _layerMasksParameters.PlayerLayer);

            var foundedTargets = new HashSet<IDamageable>();
            for (var i = 0; i < countOfColliders; i++)
            {
                var collider = _overlapResult[i];

                if (!collider.TryGetComponent(out IDamageable damageable))
                    continue;

                foundedTargets.Add(damageable);
            }

            var damage = _parameters.MeleeDamage;
            foreach (var target in foundedTargets)
                target.HandleDamage(damage);
            
            _leftCountOfAttacks--;
            
            IsCanMeleeAttack = false;

            if (_leftCountOfAttacks <= 0)
            {
                var reloadDurationSeconds = _parameters.ComboReloadDurationSeconds;
                Observable.Timer(TimeSpan.FromSeconds(reloadDurationSeconds)).Subscribe(_ =>
                    {
                        IsCanMeleeAttack = true;
                        _leftCountOfAttacks = _parameters.CountOfAttacksInCombo;
                    })
                    .AddTo(_aliveDisposable);
            }
            else
            {
                var delayBetweenAttacks = _parameters.DelayBetweenMeleeAttacks;
                Observable.Timer(TimeSpan.FromSeconds(delayBetweenAttacks)).Subscribe(_ => IsCanMeleeAttack = true)
                    .AddTo(_aliveDisposable);
            }
        }
    }
}