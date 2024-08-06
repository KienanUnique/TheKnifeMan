using Game.Utils.Directions;

namespace Game.Enemy.Parts.Attacker
{
    public interface IEnemyMeleeAttacker : IEnemyPoolPart
    {
        bool IsCanMeleeAttack { get; }
        void DamageTargets(EDirection2D attackDirection);
    }
}