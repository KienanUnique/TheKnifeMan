using Db.EnemiesParametersProvider.Parameters;

namespace Game.Enemy.Parts.Visual.Impl
{
    public class DefaultEnemyVisualPart : AEnemyVisualPart, IEnemyVisualPartBase
    {
        public DefaultEnemyVisualPart(IEnemyParametersBase parameters) : base(parameters)
        {
        }
    }
}