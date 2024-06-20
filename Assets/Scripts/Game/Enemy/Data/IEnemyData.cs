using Game.Character;
using Instance;
using UnityEngine.AI;

namespace Game.Enemy.Data
{
    public interface IEnemyData : ICharacterData
    {
        NavMeshAgent NavMeshAgent { get; }
        IBehaviourTreeInstance BehaviourTreeInstance { get; }
    }
}