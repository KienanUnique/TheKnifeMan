using Game.Object.Part;

namespace Game.Character.Parts.AnimatorStatus
{
    public interface IAnimatorStatusCheckerPart : IObjectPart
    {
        bool IsAnimatorBusy { get; }
    }
}