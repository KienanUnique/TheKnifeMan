using System;
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

        private AudioClipVo GetSoundVoByType(EUiSoundFxType type) => _soundFxBase.GetSoundVoByType(type);
        
        public void Play(EUiSoundFxType soundFxType)
        {
            var soundVo = GetSoundVoByType(soundFxType);
            var audioSource = GetAudioSourceWithSfx(soundVo);
            
            audioSource.Play();
        }

        public void Play(EUiSoundFxType soundFxType, Action onCompleteCallBack)
        {
            var soundVo = GetSoundVoByType(soundFxType);
            var audioSource = GetAudioSourceWithSfx(soundVo, onCompleteCallBack);
            
            audioSource.Play();
        }

        public void Interrupt()
        {
            foreach (var (audioSource, _) in ActiveAudioSources) audioSource.Stop();
        }
    }
}