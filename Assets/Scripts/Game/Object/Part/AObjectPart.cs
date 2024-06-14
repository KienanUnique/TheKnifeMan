using Zenject;

namespace Game.Object.Part
{
    public abstract class AObjectPart<T> : IObjectPart
    {
        [Inject] private T _view;

        protected T View => _view;

        public abstract void Initialize();
        
        public abstract void Dispose();
    }
}