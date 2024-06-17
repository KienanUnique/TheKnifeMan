using Db.EnemiesParametersProvider.Parameters.Impl;

namespace Db.EnemiesParametersProvider
{
    public interface IEnemiesParametersProvider
    {
        ISimpleEnemyParameters SimpleEnemyParameters { get; }
    }
}