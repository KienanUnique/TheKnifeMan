using Game.Level.View.Impl;
using UnityEngine;

namespace Game.Utils
{
    public class LevelViewLink : MonoBehaviour
    {
        [SerializeField] private LevelView levelView;
        
        public LevelView LevelView => levelView;
    }
}