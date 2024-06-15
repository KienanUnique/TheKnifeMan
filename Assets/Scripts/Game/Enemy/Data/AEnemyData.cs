using System;
using Game.Object;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy.Data
{
    [Serializable]
    public abstract class AEnemyData : AObjectData
    {
        [SerializeField] private Rigidbody2D mainRigidbody;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer mainSprite;
        [SerializeField] private BehaviourTreeInstance behaviourTree;
        [SerializeField] private NavMeshAgent navMeshAgent;

        public Rigidbody2D MainRigidbody => mainRigidbody;
        public Animator Animator => animator;
        public SpriteRenderer MainSprite => mainSprite;
        public IBehaviourTreeInstance BehaviourTreeInstance => behaviourTree;
        public NavMeshAgent NavMeshAgent => navMeshAgent;
    }
}