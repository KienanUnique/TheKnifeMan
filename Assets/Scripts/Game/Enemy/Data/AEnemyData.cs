using System;
using Game.Object;
using Instance;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy.Data
{
    [Serializable]
    public abstract class AEnemyData : AObjectData
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer mainSprite;
        [SerializeField] private BehaviourTreeInstance behaviourTree;
        [SerializeField] private NavMeshAgent navMeshAgent;
        
        public Animator Animator => animator;
        public SpriteRenderer MainSprite => mainSprite;
        public IBehaviourTreeInstance BehaviourTreeInstance => behaviourTree;
        public NavMeshAgent NavMeshAgent => navMeshAgent;
    }
}