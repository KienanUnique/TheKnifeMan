using System;
using KoboldUi.Element.Controller;
using UniRx;
using UnityEngine;

namespace Ui.MainMenu.Ticker
{
    public class TickerController : AUiController<TickerView>
    {
        private IDisposable _updateDisposable;
        
        private float _textFieldWith;

        protected override void OnOpen()
        {
            View.textMeshPro.text = View.text;

            var containerRect = View.container.rect;
            _textFieldWith = containerRect.width;
            
            ResetTextPosition();

            _updateDisposable?.Dispose();
            _updateDisposable = Observable.EveryUpdate().Subscribe(_ => OnUpdate()).AddTo(View);
        }

        protected override void OnClose()
        {
            _updateDisposable?.Dispose();
        }

        private void OnUpdate()
        {
            if (View.container.anchoredPosition.x < -_textFieldWith)
            {
                ResetTextPosition();
            }
            else
            {
                View.container.anchoredPosition += Vector2.left * View.speed * Time.deltaTime;
            }
        }

        private void ResetTextPosition()
        {
            View.container.anchoredPosition = new Vector2(_textFieldWith, View.container.anchoredPosition.y);
        }
    }
}