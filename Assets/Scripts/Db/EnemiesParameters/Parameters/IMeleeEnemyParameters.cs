namespace Db.EnemiesParameters.Parameters
{
    public interface IMeleeEnemyParameters : IEnemyParametersBase
    {
        int MeleeDamage { get; }
        float ComboReloadDurationSeconds { get; }
        int CountOfAttacksInCombo { get; }
        float DelayBetweenMeleeAttacks { get; }
    }
}