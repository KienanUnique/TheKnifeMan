using Game.Object.Part;
using Game.Utils.AnimatorTriggers;
using UniRx;

namespace Game.Character.Parts.AnimatorStatus.Impl
{
    public class AnimatorStatusCheckerPart : AObjectPart<ACharacterData>, IAnimatorStatusCheckerPart
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        public bool IsAnimatorBusy { get; private set; }

        public override void Initialize()
        {
            var triggers = Data.Animator.GetBehaviours<AnimatorBusyTrigger>();
            foreach (var animatorBusyTrigger in triggers)
            {
                animatorBusyTrigger.OnAnimatorBusy.Subscribe(OnAnimatorBusy).AddTo(_compositeDisposable);
            }
        }

        private void OnAnimatorBusy(bool isBusy)
        {
            IsAnimatorBusy = isBusy;
        }

        public override void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}