using Game.Interfaces;
using UniRx;

namespace Game.Enemy.Parts.Character
{
    public interface IEnemyCharacterPartBase : IDamageable
    {
        IReactiveProperty<bool> IsDead { get; }
    }
}