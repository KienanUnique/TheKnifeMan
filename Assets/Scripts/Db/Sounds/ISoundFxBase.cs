using System.Collections.Generic;
using Utils.Sounds;

namespace Db.Sounds
{
    public interface ISoundFxBase
    {
        AudioClipVo GetSoundVoByType(EGameSoundFxType type);
        AudioClipVo GetSoundVoByType(EUiSoundFxType type);
        IReadOnlyList<AudioClipVo> GetBackgroundMusic(EGameSoundFxType type);
    }
}