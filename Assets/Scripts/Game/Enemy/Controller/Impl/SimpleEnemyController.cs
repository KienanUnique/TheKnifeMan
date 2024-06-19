using Game.Enemy.Context;
using Game.Enemy.Context.Impl;
using Game.Enemy.Data.Impl;
using Game.Enemy.Parts.Character;
using Game.Enemy.Parts.Visual;
using Game.Utils;
using UnityEngine;

namespace Game.Enemy.Controller.Impl
{
    public class SimpleEnemyController : AEnemyController<SimpleEnemyData>
    {
        [SerializeField] private SimpleEnemyData data;
        
        private IEnemyCharacterPartBase _characterBase;
        private IEnemyVisualPartBase _enemyVisualBase;

        protected override SimpleEnemyData Data => data;
        protected override IEnemyCharacterPartBase CharacterBase => _characterBase;
        protected override IEnemyVisualPartBase EnemyVisualBase => _enemyVisualBase;

        protected override void ResolveParts()
        {
            _characterBase = Resolve<IEnemyCharacterPartBase>();
            _enemyVisualBase = Resolve<IEnemyVisualPartBase>();
        }

        protected override IEnemyContextBase CreateContext()
        {
            return new DefaultEnemyContext(EEnemyType.Simple, this);
        }
    }
}