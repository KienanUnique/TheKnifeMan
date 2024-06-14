using Game.Object;
using Zenject;

namespace Game.ObjectFactory
{
    public abstract class AObjectFactory<T, TView> : IFactory<T>
        where T : AObjectController<TView> where TView : AObjectView
    {
        private readonly DiContainer _container;
        private readonly UnityEngine.Object _prefab;

        protected AObjectFactory(DiContainer container, UnityEngine.Object prefab)
        {
            _container = container;
            _prefab = prefab;
        }

        public virtual T Create()
        {
            var subContainer = _container.CreateSubContainer();
            AddModules(subContainer);

            return subContainer.InstantiatePrefabForComponent<T>(_prefab);
        }

        protected abstract void AddModules(DiContainer container);
    }
}