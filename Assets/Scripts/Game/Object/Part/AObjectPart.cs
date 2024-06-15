using Zenject;

namespace Game.Object.Part
{
    public abstract class AObjectPart<T> : IObjectPart
    {
        [Inject] private T _data;

        protected T Data => _data;

        public abstract void Initialize();
        
        public abstract void Dispose();
    }
}