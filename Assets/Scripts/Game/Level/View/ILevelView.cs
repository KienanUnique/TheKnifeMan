using UnityEngine;

namespace Game.Level.View
{
    public interface ILevelView
    {
        Vector3 PlayerSpawnPoint { get; }
    }
}