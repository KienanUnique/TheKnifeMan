using System;
using Db.Player;
using Game.Object.Part;
using UniRx;

namespace Game.Player.Parts.Character
{
    public class PlayerCharacterPart : AObjectPart<PlayerData>, IPlayerCharacterPart
    {
        private readonly IPlayerParameters _playerParameters;

        private readonly ReactiveProperty<int> _health = new();
        private readonly ReactiveProperty<bool> _isDead = new();

        public IReactiveProperty<int> Health => _health;
        public IReactiveProperty<bool> IsDead => _isDead;

        public PlayerCharacterPart(IPlayerParameters playerParameters)
        {
            _playerParameters = playerParameters;
        }

        public override void Initialize()
        {
            _health.Value = _playerParameters.Health;
        }


        public override void Dispose()
        {
        }

        public void HandleDamage(int damage)
        {
            if (_isDead.Value)
                return;

            var currentHealth = _health.Value;
            var newHealth = Math.Clamp(currentHealth - damage, 0, currentHealth);

            if (newHealth == 0)
                _isDead.Value = true;

            _health.Value = newHealth;
        }
    }
}