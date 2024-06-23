using Game.Utils.Directions;

namespace Game.Enemy.Parts.Attacker
{
    public interface IEnemyMeleeAttacker : IEnemyPoolPart
    {
        void DamageTargets(EDirection2D attackDirection);
    }
}