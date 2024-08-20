using Game.Ui.Parts.Sounds;
using KoboldUi.Interfaces;
using KoboldUi.Windows;
using Ui.MainMenu.GameTitle;
using Ui.MainMenu.MainMenu;
using Ui.MainMenu.Ticker;
using UnityEngine;

namespace Ui.MainMenu
{
    public class MainMenuWindow : AWindow, IBackLogicIgnorable
    {
        [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private SettingsView settingsView;
        [SerializeField] private TickerView tickerView;
        [SerializeField] private GameTitleView gameTitleView;
        
        protected override void AddControllers()
        {
            AddController<ManMenuController, MainMenuView>(mainMenuView);
            AddController<SettingsController, SettingsView>(settingsView);
            AddController<TickerController, TickerView>(tickerView);
            AddController<GameTitleController, GameTitleView>(gameTitleView);
        }
    }
}