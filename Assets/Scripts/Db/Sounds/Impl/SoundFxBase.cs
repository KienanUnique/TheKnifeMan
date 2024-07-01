using System;
using System.Collections.Generic;
using System.Linq;
using Alchemy.Inspector;
using Alchemy.Serialization;
using Game.Utils;
using UnityEngine;
using Utils;
using Utils.Sounds;

namespace Db.Sounds.Impl
{
    [AlchemySerialize]
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(SoundFxBase), fileName = nameof(SoundFxBase))]
    public partial class SoundFxBase : ScriptableObject, ISoundFxBase
    {
        private const string LeftTrimPath = "Resources/";
        
        [TabGroup("Tab", "Game")] 
        [AlchemySerializeField, NonSerialized]
        private Dictionary<EGameSoundFxType, GameSoundFxVo> gameSounds = new();
        
        [TabGroup("Tab", "Game")] 
        [AlchemySerializeField, NonSerialized]
        private Dictionary<LevelSceneData, Dictionary<EGameSoundFxType, GameSoundFxVo>> levelSoundsOverrides = new();

        [TabGroup("Tab", "Ui")] 
        [AlchemySerializeField, NonSerialized]
        private Dictionary<EUiSoundFxType, AudioClipVo> uiSounds = new();

        [TabGroup("Tab", "Music")] 
        [AlchemySerializeField, NonSerialized]
        private List<AudioClipVo> backgroundMusic = new();
        
        public GameSoundFxVo GetGameSoundVoByType(EGameSoundFxType type, LevelSceneData currentLevel)
        {
            if (levelSoundsOverrides.ContainsKey(currentLevel) && levelSoundsOverrides[currentLevel].ContainsKey(type))
                return levelSoundsOverrides[currentLevel][type];
            
            return gameSounds[type];
        }

        public AudioClipVo GetSoundVoByType(EUiSoundFxType type) => uiSounds[type];
        public IReadOnlyList<AudioClipVo> GetBackgroundMusic() => backgroundMusic;

        [BoxGroup("GeneralVolume")]
        [SerializeField] 
        [Range(0f, 1f)]
        private float generalSound = 1f;
        
#if UNITY_EDITOR
        [Button]
        [BoxGroup("GeneralVolume")]
        public void SetSameVolumeToAllSounds()
        {
            ApplyActionToAllAudioClipsVos(vo => vo.volume = generalSound);
        }
        
        [Button]
        public void UpdateSfxPaths()
        {
            ApplyActionToAllAudioClipsVos(UpdateSoundPath);
        }

        private void ApplyActionToAllAudioClipsVos(Action<AudioClipVo> action)
        {
            foreach (var gameSoundFxVo in gameSounds.Values)
            {
                ApplyActionToAudioClipsVos(gameSoundFxVo.audioClipVo, action);
            }
            
            foreach (var soundsOverride in levelSoundsOverrides.Values)
            {
                foreach (var gameSoundFxVo in soundsOverride.Values)
                {
                    ApplyActionToAudioClipsVos(gameSoundFxVo.audioClipVo, action);
                }
            }
            
            ApplyActionToAudioClipsVos(uiSounds.Values.ToList(), action);
            
            ApplyActionToAudioClipsVos(backgroundMusic, action);
        }
        
        private void ApplyActionToAudioClipsVos(List<AudioClipVo> audioClipVos, Action<AudioClipVo> action)
        {
            foreach (var audioClipVo in audioClipVos)
            {
                action(audioClipVo);
            }
        }
        
        private void UpdateSoundPath(AudioClipVo audioClipVo)
        {
            var path = UnityEditor.AssetDatabase.GetAssetPath(audioClipVo.clip);
            path = TrimPathTo(path, LeftTrimPath);
            audioClipVo.path = path;
        }
        
        private string TrimPathTo(string path, string trimTo)
        {
            if (path.Contains(trimTo))
            {
                var startIndex = path.IndexOf(trimTo, StringComparison.Ordinal);
                startIndex += trimTo.Length;
                
                var endIndex = path.LastIndexOf(".", StringComparison.Ordinal);

                path = path[startIndex..endIndex];
            }

            return path;
        }
#endif
        
    }
}