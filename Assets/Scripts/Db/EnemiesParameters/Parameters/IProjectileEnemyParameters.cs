namespace Db.EnemiesParameters.Parameters
{
    public interface IProjectileEnemyParameters : IEnemyParametersBase
    {
        float ReloadDurationSeconds { get; }
    }
}