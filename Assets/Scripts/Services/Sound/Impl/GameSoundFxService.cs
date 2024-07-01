using Db.Sounds;
using Services.Level;
using Services.Settings;
using UnityEngine;
using Utils.Sounds;

namespace Services.Sound.Impl
{
    public class GameSoundFxService : ASoundFxService<EGameSoundFxType>, IGameSoundFxService
    {
        private readonly ISoundFxBase _soundFxBase;
        private readonly ILevelsService _levelsService;
        
        public GameSoundFxService(
            ISettingsStorageService settingsStorageService, 
            IAudioSourcePool audioSourcePool, 
            IAudioClipRepository audioClipRepository,
            ISoundFxBase soundFxBase,
            ILevelsService levelsService
        ) : base(settingsStorageService, audioSourcePool, audioClipRepository)
        {
            _soundFxBase = soundFxBase;
            _levelsService = levelsService;
        }

        protected override AudioClipVo GetSoundVoByType(EGameSoundFxType type)
        {
            var gameSound = _soundFxBase.GetGameSoundVoByType(type, _levelsService.CurrentLevelData);
            var randomSoundIndex = Random.Range(0, gameSound.audioClipVo.Count - 1);
            
            return gameSound.audioClipVo[randomSoundIndex];
        }
    }
}