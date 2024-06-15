using Alchemy.Inspector;
using Game.Player;
using UnityEngine;

namespace Game.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private PlayerSpawnPoint playerSpawnPoint;

        public Vector3 PlayerSpawnPoint => playerSpawnPoint.transform.position;

        [Button]
        public void AutoFill()
        {
            playerSpawnPoint = FindObjectOfType<PlayerSpawnPoint>();
        }
    }
}