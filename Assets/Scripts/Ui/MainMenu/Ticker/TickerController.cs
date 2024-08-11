using System;
using KoboldUi.Element.Controller;
using UniRx;
using UnityEngine;

namespace Ui.MainMenu.Ticker
{
    public class TickerController : AUiController<TickerView>
    {
        private CompositeDisposable _compositeDisposable;
        
        private float _textFieldWith;

        protected override void OnOpen()
        {
            View.textMeshPro.text = View.text;

            var containerRect = View.container.rect;
            _textFieldWith = containerRect.width;
            
            ResetTextPosition();
            
            _compositeDisposable?.Dispose();
            
            _compositeDisposable = new CompositeDisposable();
            Observable.EveryUpdate().Subscribe(_ => OnUpdate()).AddTo(_compositeDisposable, View);
            Observable.Interval(TimeSpan.FromSeconds(View.durationSeconds)).Subscribe(_ => ResetTextPosition())
                .AddTo(_compositeDisposable, View);
        }

        protected override void OnClose()
        {
            _compositeDisposable?.Dispose();
        }

        private void OnUpdate()
        {
            View.container.anchoredPosition += Vector2.left * View.speed * Time.deltaTime;
        }

        private void ResetTextPosition()
        {
            View.container.anchoredPosition = new Vector2(_textFieldWith / 2, View.container.anchoredPosition.y);
        }
    }
}