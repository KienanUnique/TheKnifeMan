using Game.Ui.Abstract.AnyKey;
using Services.Level;

namespace Game.Ui.WinWindow.AnyKey
{
    public class WinPressAnyKeyController : APressAnyKeyController<WinPressAnyKeyView>
    {
        private readonly ILevelsService _levelsService;

        public WinPressAnyKeyController(ILevelsService levelsService)
        {
            _levelsService = levelsService;
        }

        protected override void OnAnyKeyPressed()
        {
            _levelsService.LoadNextLevel();
        }
    }
}