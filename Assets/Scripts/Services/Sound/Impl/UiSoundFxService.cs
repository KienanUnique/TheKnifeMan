using Db.Sounds;
using Services.Settings;
using Utils.Sounds;

namespace Services.Sound.Impl
{
    public class UiSoundFxService : ASoundFxService<EUiSoundFxType>, IUiSoundFxService
    {
        private readonly ISoundFxBase _soundFxBase;
        
        public UiSoundFxService(
            ISettingsStorageService settingsStorageService, 
            IAudioSourcePool audioSourcePool, 
            IAudioClipRepository audioClipRepository,
            ISoundFxBase soundFxBase
        ) : base(settingsStorageService, audioSourcePool, audioClipRepository)
        {
            _soundFxBase = soundFxBase;
        }

        protected override AudioClipVo GetSoundVoByType(EUiSoundFxType type) => _soundFxBase.GetSoundVoByType(type);
    }
}