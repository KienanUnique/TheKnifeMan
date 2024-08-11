using KoboldUi.Element.Controller;
using UnityEngine;

namespace Ui.Loading.Loading
{
    public class LoadingController : AUiController<LoadingView>
    {
        protected override void OnOpen()
        {
            View.backgroundImage.sprite = SelectRandomBackground();
        }

        private Sprite SelectRandomBackground()
        {
            var backgroundIndex = Random.Range(0, View.images.Count);
            return View.images[backgroundIndex];
        }
    }
}