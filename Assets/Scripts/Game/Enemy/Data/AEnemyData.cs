using System;
using Game.Character;
using Instance;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy.Data
{
    [Serializable]
    public abstract class AEnemyData : ACharacterData, IEnemyData
    {
        [SerializeField] private BehaviourTreeInstance behaviourTree;
        [SerializeField] private NavMeshAgent navMeshAgent;
        
        public IBehaviourTreeInstance BehaviourTreeInstance => behaviourTree;
        public NavMeshAgent NavMeshAgent => navMeshAgent;

        public override void AutoFill()
        {
            base.AutoFill();
            behaviourTree = rootTransform.GetComponent<BehaviourTreeInstance>();
            navMeshAgent = rootTransform.GetComponent<NavMeshAgent>();
        }
    }
}