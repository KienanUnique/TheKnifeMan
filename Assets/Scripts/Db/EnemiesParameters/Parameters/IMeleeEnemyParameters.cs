namespace Db.EnemiesParameters.Parameters
{
    public interface IMeleeEnemyParameters : IEnemyParametersBase
    {
        int MeleeDamage { get; }
    }
}