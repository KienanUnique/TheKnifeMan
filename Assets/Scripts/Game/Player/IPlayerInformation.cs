using UniRx;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayerInformation
    {
        IReactiveProperty<bool> IsInitilized { get; }
        Transform Transform { get; }
        IReactiveProperty<int> Health { get; }
        IReactiveProperty<bool> IsDead { get; }
    }
}