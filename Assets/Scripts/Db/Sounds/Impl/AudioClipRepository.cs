using System;
using System.Collections.Generic;
using UnityEngine;

namespace Db.Sounds.Impl
{
    public class AudioClipRepository : IAudioClipRepository
    {
        private readonly Dictionary<string, AudioClip> _fullPathToClipMap = new();

        public AudioClip GetClipByName(string name)
        {
            if (_fullPathToClipMap.TryGetValue(name, out var audioClip))
                return audioClip;

            audioClip = Resources.Load<AudioClip>(name);
            if (audioClip == null)
                throw new ArgumentException("Audio clip <" + name + "> does not exist");

            _fullPathToClipMap.Add(name, audioClip);
            return audioClip;
        }
    }
}