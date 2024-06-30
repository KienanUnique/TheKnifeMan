using System;
using System.Collections.Generic;
using Alchemy.Inspector;
using Alchemy.Serialization;
using UnityEngine;
using Utils;
using Utils.Sounds;

namespace Db.Sounds.Impl
{
    [AlchemySerialize]
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(SoundFxBase), fileName = nameof(SoundFxBase))]
    public partial class SoundFxBase : ScriptableObject
    {
        [TabGroup("Tab", "Game")] 
        [AlchemySerializeField, NonSerialized]
        private Dictionary<EGameSoundFxType, AudioClipVo> gameSounds = new();

        [TabGroup("Tab", "Ui")] 
        [AlchemySerializeField, NonSerialized]
        private Dictionary<EUiSoundFxType, AudioClipVo> uiSounds = new();

        [TabGroup("Tab", "Music")] 
        [AlchemySerializeField, NonSerialized]
        private List<AudioClipVo> backgroundMusic = new();
        
        public AudioClipVo GetSoundVoByType(EGameSoundFxType type) => gameSounds[type];
        public AudioClipVo GetSoundVoByType(EUiSoundFxType type) => uiSounds[type];
        public IReadOnlyList<AudioClipVo> GetBackgroundMusic(EGameSoundFxType type) => backgroundMusic;
    }
}