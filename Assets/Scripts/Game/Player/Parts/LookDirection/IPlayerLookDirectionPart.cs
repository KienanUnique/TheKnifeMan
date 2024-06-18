using Game.Object.Part;
using Game.Utils.Directions;
using UniRx;

namespace Game.Player.Parts.LookDirection
{
    public interface IPlayerLookDirectionPart : IObjectPart
    {
        IReactiveProperty<EDirection1D> LookDirection1D { get; }
        
        EDirection2D CalculateLookDirection2D();
    }
}