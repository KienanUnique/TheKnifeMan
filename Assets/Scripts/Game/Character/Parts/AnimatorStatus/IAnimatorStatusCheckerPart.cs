using Game.Object.Part;
using UniRx;

namespace Game.Character.Parts.AnimatorStatus
{
    public interface IAnimatorStatusCheckerPart : IObjectPart
    {
        bool IsAnimatorBusy { get; }
        IReactiveCommand<bool> IsAnimatorBusyChanged { get; }
    }
}