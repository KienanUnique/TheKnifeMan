using Game.Object.PartsFactory;
using Zenject;

namespace Game.Enemy.PartsFactory
{
    public abstract class AEnemyPartsFactory : APartsFactory
    {
        protected AEnemyPartsFactory(DiContainer mainContainer) : base(mainContainer)
        {
        }
    }
}