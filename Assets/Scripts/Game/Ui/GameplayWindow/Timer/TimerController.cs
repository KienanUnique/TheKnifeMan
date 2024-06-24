using System;
using Game.Services.WaveTimer;
using KoboldUi.Element.Controller;
using UniRx;
using UnityEngine;

namespace Game.Ui.GameplayWindow.Timer
{
    public class TimerController : AUiController<TimerView>
    {
        private readonly IWaveTimerService _waveTimerService;
        
        private Color _initialColor;
        private bool _isTimerBlocked;

        public TimerController(IWaveTimerService waveTimerService)
        {
            _waveTimerService = waveTimerService;
        }

        public override void Initialize()
        {
            _waveTimerService.RemainingTime.Subscribe(UpdateTimer).AddTo(View);
            _waveTimerService.OnTimerEnd.Subscribe(_ => OnTimerEnd()).AddTo(View);

            _initialColor = View.timerText.color;
            
            UpdateTimer(_waveTimerService.RemainingTime.Value);
        }

        private void UpdateTimer(TimeSpan timeSpan)
        {
            if(_isTimerBlocked)
                return;
            
            View.timerText.text = $"{timeSpan.Minutes}:{timeSpan.Seconds}";
        }

        private void OnTimerEnd()
        {
            if(View.countOfBlinks == 0)
                return;

            _isTimerBlocked = true;
            
            var countOfTicks = View.countOfBlinks * 2 - 1;
            
            Observable.Timer(TimeSpan.FromSeconds(View.visibleDelay * countOfTicks))
                .Subscribe(_ =>
                {
                    _isTimerBlocked = false;
                    
                    if(_waveTimerService.RemainingTime.Value != TimeSpan.Zero)
                        UpdateTimer(_waveTimerService.RemainingTime.Value);
                    else
                        View.timerText.text = "--:--";
                }).AddTo(View);
            
            View.timerText.color = View.timerEndColor;
            
            Observable.Interval(TimeSpan.FromSeconds(View.visibleDelay))
                .Take(countOfTicks)
                .Subscribe(x =>
                {
                    Color nextColor;
                    if (x % 2 == 0)
                        nextColor = _initialColor;
                    else
                        nextColor = View.timerEndColor;
                    View.timerText.color = nextColor;
                })
                .AddTo(View);
        }
    }
}