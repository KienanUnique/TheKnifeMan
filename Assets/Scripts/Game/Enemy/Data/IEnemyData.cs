using Instance;
using UnityEngine.AI;

namespace Game.Enemy.Data
{
    public interface IEnemyData
    {
        NavMeshAgent NavMeshAgent { get; }
        IBehaviourTreeInstance BehaviourTreeInstance { get; }
    }
}