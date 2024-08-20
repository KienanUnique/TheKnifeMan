using Game.Object.Part;
using Zenject;

namespace Game.Object.PartsFactory
{
    public abstract class APartsFactory : IPartsFactory
    {
        private readonly DiContainer _mainContainer;
        private DiContainer _localContainer;

        protected APartsFactory(DiContainer mainContainer)
        {
            _mainContainer = mainContainer;
        }

        public void CreateParts(object[] extraArgs)
        {
            _localContainer = _mainContainer.CreateSubContainer();
            HandleCreateParts(_localContainer, extraArgs);
        }

        public T Resolve<T>() where T : IObjectPart
        {
            return _localContainer.Resolve<T>();
        }

        protected abstract void HandleCreateParts(DiContainer container, object[] extraArgs);
    }
}