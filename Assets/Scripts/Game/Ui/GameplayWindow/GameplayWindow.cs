using Game.Ui.GameplayWindow.Health;
using Game.Ui.GameplayWindow.Score;
using Game.Ui.GameplayWindow.Timer;
using KoboldUi.Interfaces;
using KoboldUi.Windows;
using UnityEngine;

namespace Game.Ui.GameplayWindow
{
    public class GameplayWindow : AWindow, IBackLogicIgnorable
    {
        [SerializeField] private ScoreCounterView scoreCounterView;
        [SerializeField] private HealthView healthView;
        [SerializeField] private TimerView timerView;
        
        protected override void AddControllers()
        {
            AddController<ScoreCounterController, ScoreCounterView>(scoreCounterView);
            AddController<HealthController, HealthView>(healthView);
            AddController<TimerController, TimerView>(timerView);
        }
    }
}