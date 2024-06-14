using System;
using System.Collections.Generic;
using Game.Object.Part;
using Game.Object.PartsFactory;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Object
{
    public abstract class AObjectController<TView> : MonoBehaviour, IInitializable, IDisposable
        where TView : AObjectView
    {
        protected readonly CompositeDisposable CompositeDisposable = new();

        [Inject] private IPartsFactory _partsFactory;

        private readonly List<IObjectPart> _parts = new();

        protected virtual object[] ExtraArgsForParts => new object[] {View};
        protected TView View { get; private set; }


        public void Initialize()
        {
            View = GetComponent<TView>();

            _partsFactory.CreateParts(ExtraArgsForParts);
            ResolveParts();
            InitializeParts();

            HandleInitialize();
        }

        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }

        protected abstract void ResolveParts();

        protected virtual void HandleInitialize()
        {
        }

        protected T Resolve<T>() where T : IObjectPart
        {
            var part = _partsFactory.Resolve<T>();
            _parts.Add(part);

            return part;
        }

        private void InitializeParts()
        {
            foreach (var objectPart in _parts)
            {
                CompositeDisposable.Add(objectPart);
                objectPart.Initialize();
            }
        }
    }
}