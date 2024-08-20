namespace Game.Enemy.ActionsExecutor
{
    public interface IMeleeAttackEnemy : IDefaultControllableEnemy
    {
        bool IsCanMeleeAttack { get; }
        void AttackMelee();
    }
}