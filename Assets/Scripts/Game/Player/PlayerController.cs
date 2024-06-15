using Db.Player;
using Game.Interfaces;
using Game.Object;
using Game.Player.Parts.Character;
using Game.Player.Parts.Movement;
using Game.Player.Parts.Visual;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerController : AObjectController<PlayerData>, IPlayerInformation, IDamageable
    {
        [SerializeField] private PlayerData data;

        [Inject] private IPlayerParameters _parameters;
        
        private IPlayerMovementPart _movement;
        private IPlayerVisualPart _visual;
        private IPlayerCharacterPart _character;

        public Transform Transform => transform;
        public IReactiveProperty<int> Health => _character.Health;
        public IReactiveProperty<bool> IsDead => _character.IsDead;

        protected override PlayerData Data => data;
        
        public void HandleDamage(int damage) => _character.HandleDamage(damage);

        protected override void ResolveParts()
        {
            _movement = Resolve<IPlayerMovementPart>();
            _visual = Resolve<IPlayerVisualPart>();
            _character = Resolve<IPlayerCharacterPart>();
        }

        protected override void HandleInitialize()
        {
            _character.IsDead.Subscribe(OnDead).AddTo(CompositeDisposable);
            _character.Health.Subscribe(OnHealth).AddTo(CompositeDisposable);
            
            _movement.Enable();
        }

        private void OnHealth(int health)
        {
            if(_character.IsDead.Value)
                return;
            
            var lowHealthThreshold = _parameters.LowHealthThreshold;
            if (health > lowHealthThreshold) return;
            
            _visual.PlayInjuredAnimation();
        }

        private void OnDead(bool isDead)
        {
            if(!isDead)
                return;
            
            _visual.PlayDeathAnimation();
        }
    }
}