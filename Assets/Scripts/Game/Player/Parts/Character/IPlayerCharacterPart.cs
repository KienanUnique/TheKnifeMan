using Game.Interfaces;
using Game.Object.Part;
using UniRx;

namespace Game.Player.Parts.Character
{
    public interface IPlayerCharacterPart : IObjectPart, IDamageable
    {
        IReactiveProperty<int> Health { get; }
        IReactiveProperty<bool> IsDead { get; }
    }
}