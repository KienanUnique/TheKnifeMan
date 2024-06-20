using System;
using Db.EnemiesParametersProvider.Parameters;
using Game.Enemy.Data;
using Game.Object.Part;
using UniRx;

namespace Game.Enemy.Parts.Character.Impl
{
    public class DefaultEnemyCharacterPart : AObjectPart<IEnemyData>, IEnemyCharacterPartBase
    {
        private readonly IEnemyParametersBase _enemyParameters;
        
        private readonly ReactiveProperty<int> _health = new();
        private readonly ReactiveProperty<bool> _isDead = new();

        public int Health => _health.Value;
        public IReactiveProperty<bool> IsDead => _isDead;

        public DefaultEnemyCharacterPart(IEnemyParametersBase enemyParameters)
        {
            _enemyParameters = enemyParameters;
        }

        public override void Initialize()
        {
        }
        

        public override void Dispose()
        {
        }
        
        public void Enable()
        {
            _isDead.Value = false;
            _health.Value = _enemyParameters.Health;
        }

        public void DisableAndReset()
        {
        }

        public void HandleDamage(int damage)
        {
            if(_isDead.Value)
                return;

            var currentHealth = _health.Value;
            var newHealth = Math.Clamp(currentHealth - damage, 0, currentHealth);
            
            if (newHealth == 0)
                _isDead.Value = true;

            _health.Value = newHealth;
        }
    }
}