using Game.Services.Score;
using KoboldUi.Element.Controller;
using UniRx;

namespace Game.Ui.GameplayWindow.Score
{
    public class ScoreCounterController : AUiController<ScoreCounterView>
    {
        private readonly IScoreService _scoreService;

        public ScoreCounterController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        public override void Initialize()
        {
            _scoreService.CurrentScore.Subscribe(ShowScore).AddTo(View);
            ShowScore(_scoreService.CurrentScore.Value);
        }

        private void ShowScore(int newScore)
        {
            View.score.text = $"{newScore} / {_scoreService.NeedScore}";
        }
    }
}