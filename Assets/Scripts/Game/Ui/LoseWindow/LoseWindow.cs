using Game.Ui.LoseWindow.AnyKey;
using Game.Ui.LoseWindow.Lose;
using KoboldUi.Windows;
using UnityEngine;

namespace Game.Ui.LoseWindow
{
    public class LoseWindow : AWindow
    {
        [SerializeField] private LosePressAnyKeyView losePressAnyKeyView;
        [SerializeField] private LoseView loseView;

        protected override void AddControllers()
        {
            AddController<LosePressAnyKeyController, LosePressAnyKeyView>(losePressAnyKeyView);
            AddController<LoseController, LoseView>(loseView);
        }
    }
}