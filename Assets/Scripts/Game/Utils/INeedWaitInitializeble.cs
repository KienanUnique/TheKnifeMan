using UniRx;
using Zenject;

namespace Game.Utils
{
    public interface INeedWaitInitializable : IInitializable
    {
        IReactiveProperty<bool> IsInitilized { get; }
    }
}