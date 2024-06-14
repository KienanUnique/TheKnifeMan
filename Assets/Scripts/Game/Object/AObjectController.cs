using System;
using System.Collections.Generic;
using Game.Object.Part;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Object
{
    public abstract class AObjectController<TView> : MonoBehaviour, IInitializable, IDisposable
        where TView : AObjectView
    {
        protected readonly CompositeDisposable CompositeDisposable = new();
        protected TView View { get; private set; }

        public void Initialize()
        {
            View = GetComponent<TView>();
            
            var parts = CreateParts(View);
            InitializeParts(parts);
            
            HandleInitialize();
        }

        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }

        protected abstract List<IObjectPart> CreateParts(TView view);

        protected virtual void HandleInitialize()
        {
        }

        private void InitializeParts(List<IObjectPart> parts)
        {
            foreach (var objectPart in parts)
            {
                CompositeDisposable.Add(objectPart);
                objectPart.Initialize();
            }
        }
    }
}