using Game.Utils.AnimatorTriggers;
using UnityEngine;

namespace Game.Utils
{
    public readonly struct SpawnEffectData
    {
        public readonly Animator Animator;
        public readonly SpawnEffectEndedTrigger EndTrigger;
        public readonly SpawnMomentTrigger SpawnTrigger;

        public SpawnEffectData(
            Animator animator, 
            SpawnEffectEndedTrigger endTrigger, 
            SpawnMomentTrigger spawnTrigger)
        {
            Animator = animator;
            EndTrigger = endTrigger;
            SpawnTrigger = spawnTrigger;
        }
    }
}