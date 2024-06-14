using Game.Object.Part;

namespace Game.Player.Parts.Movement
{
    public interface IPlayerMovementPart : IObjectPart
    {
        void Enable();
        void Disable();
    }
}