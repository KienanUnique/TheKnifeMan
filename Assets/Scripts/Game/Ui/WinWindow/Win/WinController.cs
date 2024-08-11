using KoboldUi.Element.Controller;
using Services.Sound;
using Utils.Sounds;

namespace Game.Ui.WinWindow.Win
{
    public class WinController : AUiController<WinView>
    {
        private readonly IUiSoundFxService _uiSoundFxService;

        public WinController(IUiSoundFxService uiSoundFxService)
        {
            _uiSoundFxService = uiSoundFxService;
        }

        protected override void OnOpen()
        {
            _uiSoundFxService.Play(EUiSoundFxType.TheKnifeMan);
        }
    }
}