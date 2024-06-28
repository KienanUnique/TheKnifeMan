using KoboldUi.Interfaces;
using KoboldUi.Windows;
using Ui.MainMenu.Controller;
using Ui.MainMenu.View;
using UnityEngine;

namespace Ui.MainMenu
{
    public class MainMenuWindow : AWindow, IBackLogicIgnorable
    {
        [SerializeField] private MainMenuView mainMenuView;
        
        protected override void AddControllers()
        {
            AddController<ManMenuController, MainMenuView>(mainMenuView);
        }
    }
}