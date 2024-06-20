using Game.Utils.Directions;
using UniRx;

namespace Game.Enemy.Parts.LookDirection
{
    public interface IEnemyLookDirectionPart : IEnemyPoolPart
    {
        IReactiveProperty<EDirection1D> LookDirection1D { get; }

        EDirection2D CalculateLookDirection2D();
    }
}