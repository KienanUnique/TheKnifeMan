using Game.Ui.Abstract.AnyKey;
using Services.Level;

namespace Game.Ui.LoseWindow.AnyKey
{
    public class LosePressAnyKeyController : APressAnyKeyController<LosePressAnyKeyView>
    {
        private readonly ILevelsService _levelsService;

        public LosePressAnyKeyController(ILevelsService levelsService)
        {
            _levelsService = levelsService;
        }

        protected override void OnAnyKeyPressed()
        {
            _levelsService.ReloadLevel();
        }
    }
}