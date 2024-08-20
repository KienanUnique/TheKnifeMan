using System.Collections.Generic;
using Alchemy.Inspector;
using KoboldUi.Element.View;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Loading.Loading
{
    public class LoadingView : AUiAnimatedView
    {
        [Header("Links")]
        public Image backgroundImage;
        
        [Header("Images")]
        [AssetsOnly] public List<Sprite> images;
    }
}