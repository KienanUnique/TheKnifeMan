using Db.EnemiesParametersProvider;
using Db.EnemiesParametersProvider.Parameters.Impl;
using Zenject;

namespace Game.Enemy.PartsFactory.Impl
{
    public class SimpleEnemyPartsFactory : AMeleeEnemyPartsFactory<ISimpleEnemyParameters>
    {
        public SimpleEnemyPartsFactory(
            DiContainer mainContainer,
            IEnemiesParametersProvider enemiesParametersProvider
        ) : base(mainContainer, enemiesParametersProvider)
        {
        }

        protected override ISimpleEnemyParameters GetParameters(IEnemiesParametersProvider enemiesParametersProvider)
        {
            return enemiesParametersProvider.SimpleEnemyParameters;
        }
    }
}