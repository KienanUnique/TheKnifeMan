using DG.Tweening;
using KoboldUi.Element.Controller;

namespace Ui.MainMenu.GameTitle
{
    public class GameTitleController : AUiController<GameTitleView>
    {
        private Tween _animationTween;

        protected override void OnOpen()
        {
            _animationTween?.Kill();
            
            _animationTween = View.container.DOPunchScale(View.scalePunch, View.duration, View.vibrato, View.elasticity)
                .SetEase(View.ease)
                .SetLoops(-1, LoopType.Restart)
                .SetLink(View.gameObject);
        }

        protected override void OnClose()
        {
            _animationTween?.Kill();
        }
    }
}