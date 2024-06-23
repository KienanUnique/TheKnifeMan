using Db.EnemiesParameters;
using Db.EnemiesParameters.Parameters;

namespace Game.Enemy.Parts.Visual.Impl
{
    public class DefaultEnemyVisualPart : AEnemyVisualPart, IEnemyVisualPartBase
    {
        public DefaultEnemyVisualPart(IEnemyParametersBase parameters) : base(parameters)
        {
        }
    }
}