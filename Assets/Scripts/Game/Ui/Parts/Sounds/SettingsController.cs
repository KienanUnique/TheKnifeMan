using KoboldUi.Element.Controller;
using Services.Settings;
using Services.Sound;
using UniRx;
using Utils.Sounds;

namespace Game.Ui.Parts.Sounds
{
    public class SettingsController : AUiController<SettingsView>
    {
        private readonly ISettingsStorageService _settingsStorageService;
        private readonly IUiSoundFxService _uiSoundFxService;

        private bool _isWaitingTestSoundEnd;

        public SettingsController(
            ISettingsStorageService settingsStorageService, 
            IUiSoundFxService uiSoundFxService
        )
        {
            _settingsStorageService = settingsStorageService;
            _uiSoundFxService = uiSoundFxService;
        }

        public override void Initialize()
        {
            View.musicToggle.isOn = _settingsStorageService.IsMusicEnabled.Value;
            View.soundToggle.isOn = _settingsStorageService.IsSoundsEnabled.Value;
            
            View.musicVolume.value = _settingsStorageService.MusicVolume.Value;
            View.soundVolume.value = _settingsStorageService.SoundsVolume.Value;
            
            View.playTestSound.OnClickAsObservable().Subscribe(_ => OnPlayTestSound()).AddTo(View);
            
            View.musicToggle.OnValueChangedAsObservable().Subscribe(OnMusicToggleValueChanged).AddTo(View);
            View.musicVolume.OnValueChangedAsObservable().Subscribe(OnMusicVolumeChanged).AddTo(View);
            
            View.soundToggle.OnValueChangedAsObservable().Subscribe(OnSoundToggleValueChanged).AddTo(View);
            View.soundVolume.OnValueChangedAsObservable().Subscribe(OnSoundVolumeChanged).AddTo(View);
        }

        private void OnMusicVolumeChanged(float volume)
        {
            _settingsStorageService.SetMusicVolume(volume);
        }
        private void OnMusicToggleValueChanged(bool isOn)
        {
            _settingsStorageService.SetIsMusicEnabled(isOn);
            View.musicVolume.interactable = isOn;
        }
        
        private void OnSoundToggleValueChanged(bool isOn)
        {
            _settingsStorageService.SetIsSoundsEnabled(isOn);
            
            View.playTestSound.interactable = isOn && !_isWaitingTestSoundEnd;
            View.soundVolume.interactable = isOn;
        }
        private void OnSoundVolumeChanged(float volume)
        {
            _settingsStorageService.SetSoundsVolume(volume);
        }

        private void OnPlayTestSound()
        {
            if(_isWaitingTestSoundEnd)
                return;
            
            View.playTestSound.interactable = false;
            _isWaitingTestSoundEnd = true;
            _uiSoundFxService.Play(EUiSoundFxType.SoundCheck, () =>
            {
                View.playTestSound.interactable = true;
                _isWaitingTestSoundEnd = false;
            });
        }
    }
}