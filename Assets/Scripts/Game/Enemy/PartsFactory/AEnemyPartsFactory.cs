using Db.EnemiesParametersProvider;
using Game.Object.PartsFactory;
using Zenject;

namespace Game.Enemy.PartsFactory
{
    public abstract class AEnemyPartsFactory : APartsFactory
    {
        private readonly IEnemiesParametersProvider _enemiesParametersProvider;

        protected AEnemyPartsFactory(
            DiContainer mainContainer,
            IEnemiesParametersProvider enemiesParametersProvider
        ) : base(mainContainer)
        {
            _enemiesParametersProvider = enemiesParametersProvider;
        }

        protected abstract void HandleCreateParts(DiContainer container, object[] extraArgs,
            IEnemiesParametersProvider enemiesParametersProvider);

        protected sealed override void HandleCreateParts(DiContainer container, object[] extraArgs)
        {
            HandleCreateParts(container, extraArgs, _enemiesParametersProvider);
        }
    }
}