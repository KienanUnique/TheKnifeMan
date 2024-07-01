using System.Collections.Generic;
using Game.Utils;
using Utils.Sounds;

namespace Db.Sounds
{
    public interface ISoundFxBase
    {
        GameSoundFxVo GetGameSoundVoByType(EGameSoundFxType type, LevelSceneData currentLevel);
        AudioClipVo GetSoundVoByType(EUiSoundFxType type);
        IReadOnlyList<AudioClipVo> GetBackgroundMusic(EGameSoundFxType type);
    }
}