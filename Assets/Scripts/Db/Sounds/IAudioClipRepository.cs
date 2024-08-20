using UnityEngine;

namespace Db.Sounds
{
    public interface IAudioClipRepository
    {
        public AudioClip GetClipByName(string name);
    }
}