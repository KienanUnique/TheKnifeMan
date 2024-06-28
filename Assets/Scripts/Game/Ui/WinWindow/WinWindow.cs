using Game.Ui.WinWindow.AnyKey;
using Game.Ui.WinWindow.Win;
using KoboldUi.Windows;
using UnityEngine;

namespace Game.Ui.WinWindow
{
    public class WinWindow : AWindow
    {
        [SerializeField] private WinPressAnyKeyView winPressAnyKeyView;
        [SerializeField] private WinView winView;

        protected override void AddControllers()
        {
            AddController<WinPressAnyKeyController, WinPressAnyKeyView>(winPressAnyKeyView);
            AddController<WinController, WinView>(winView);
        }
    }
}