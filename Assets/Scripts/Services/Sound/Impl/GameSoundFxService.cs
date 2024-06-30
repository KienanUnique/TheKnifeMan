using Db.Sounds;
using Services.Settings;
using Utils.Sounds;

namespace Services.Sound.Impl
{
    public class GameSoundFxService : ASoundFxService<EGameSoundFxType>
    {
        private readonly ISoundFxBase _soundFxBase;
        
        public GameSoundFxService(
            ISettingsStorageService settingsStorageService, 
            IAudioSourcePool audioSourcePool, 
            IAudioClipRepository audioClipRepository,
            ISoundFxBase soundFxBase
        ) : base(settingsStorageService, audioSourcePool, audioClipRepository)
        {
        }

        protected override AudioClipVo GetSoundVoByType(EGameSoundFxType type) => _soundFxBase.GetSoundVoByType(type);
    }
}