using System.Collections.Generic;
using UnityEngine;

namespace Services.Sound.Impl
{
    public class AudioSourcePool : IAudioSourcePool
    {
        private readonly Stack<AudioSource> _pool = new();

        private Transform _rootTransform;

        public AudioSource Get()
        {
            AudioSource source;

            while (_pool.TryPop(out source) && source == null)
            {
            }
            
            if (_pool.Count == 0 && source == null)
            {
                CreatePoolElement();
                source = _pool.Pop();
            }
            
            source.gameObject.SetActive(true);

            return source;
        }

        public void Return(AudioSource source)
        {
            source.gameObject.SetActive(false);

            source.Stop();
            source.loop = false;

            var sourceTransform = source.transform;
            sourceTransform.SetParent(_rootTransform);
            sourceTransform.position = Vector3.zero;

            _pool.Push(source);
        }

        public void CreatePoolElement()
        {
            if (_rootTransform == null)
            {
                _rootTransform = new GameObject("AudioSources").transform; 
            }
            
            var gameObject = new GameObject("AudioSource");
            var source = gameObject.AddComponent<AudioSource>();
            gameObject.transform.SetParent(_rootTransform);
            Object.DontDestroyOnLoad(gameObject); // TODO: refactor this

            Return(source);
        }
    }
}