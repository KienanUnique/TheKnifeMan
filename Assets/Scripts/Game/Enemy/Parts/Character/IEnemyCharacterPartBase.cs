using Game.Interfaces;
using UniRx;

namespace Game.Enemy.Parts.Character
{
    public interface IEnemyCharacterPartBase : IEnemyPoolPart, IDamageable
    {
        int Health { get; }
        IReactiveProperty<bool> IsDead { get; }
    }
}