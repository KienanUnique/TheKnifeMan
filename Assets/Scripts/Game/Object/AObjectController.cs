﻿using System.Collections.Generic;
using Game.Object.Part;
using Game.Object.PartsFactory;
using UniRx;
using Zenject;

namespace Game.Object
{
    public abstract class AObjectController<TData> : AObjectControllerBase, IInitializable
        where TData : AObjectData
    {
        protected readonly CompositeDisposable CompositeDisposable = new();

        [Inject] private IPartsFactory _partsFactory;

        private readonly List<IObjectPart> _parts = new();

        protected virtual object[] ExtraArgsForParts => new object[] {Data};
        protected abstract TData Data { get; }

        public void Initialize()
        {
            _partsFactory.CreateParts(ExtraArgsForParts);
            ResolveParts();
            InitializeParts();

            HandleInitialize();
        }

        protected virtual void ResolveParts()
        {
        }

        protected virtual void HandleInitialize()
        {
        }

        protected virtual T Resolve<T>() where T : IObjectPart
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

        private void OnDestroy()
        {
            CompositeDisposable?.Dispose();
        }
    }
}