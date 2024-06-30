using UnityEngine;

namespace Services.Sound
{
    public interface IAudioSourcePool
    {
        AudioSource Get();
        void Return(AudioSource source);
        void CreatePoolElement();
    }
}