using System;
using System.Collections.Generic;
using Game.Enemy.ActionsExecutor;
using Game.Enemy.Context;
using Game.Enemy.Data;
using Game.Enemy.Parts;
using Game.Enemy.Parts.Character;
using Game.Enemy.Parts.Visual;
using Game.Interfaces;
using Game.Object;
using UniRx;
using UnityEngine;

namespace Game.Enemy.Controller
{
    public abstract class AEnemyController<TData> : AObjectController<TData>, IDefaultActionsExecutor, IPoolEnemy,
        IDamageable where TData : AEnemyData
    {
        private CompositeDisposable _aliveDisposables;
        private readonly List<IEnemyPoolPart> _poolParts = new();
        private readonly ReactiveCommand<IPoolEnemy> _onDead = new();

        private IEnemyContextBase _context;

        public IObservable<IPoolEnemy> OnDead => _onDead;

        protected abstract IEnemyCharacterPartBase CharacterBase { get; }
        protected abstract IEnemyVisualPartBase EnemyVisualBase { get; }
        protected CompositeDisposable AliveDisposables => _aliveDisposables;

        public virtual void HandleEnable()
        {
            _aliveDisposables?.Dispose();
            _aliveDisposables = new CompositeDisposable();
            
            gameObject.SetActive(true);
            foreach (var poolPart in _poolParts)
            {
                poolPart.Enable();
            }

            Observable.EveryUpdate().Subscribe(_ => OnUpdate()).AddTo(_aliveDisposables);
            
            Data.NavMeshAgent.isStopped = false;
        }

        public virtual void HandleDisableAndReset()
        {
            gameObject.SetActive(false);
            
            foreach (var poolPart in _poolParts)
            {
                poolPart.DisableAndReset();
            }

            Data.BehaviourTreeInstance.Reset();
        }
        
        public void HandleDamage(int damage)
        {
            CharacterBase.HandleDamage(damage);
        }

        protected abstract IEnemyContextBase CreateContext();

        protected virtual void OnInitialize()
        {
        }

        protected sealed override void HandleInitialize()
        {
            CharacterBase.IsDead.Subscribe(OnIsDead).AddTo(CompositeDisposable);
            
            Data.NavMeshAgent.updateRotation = false;
            Data.NavMeshAgent.updateUpAxis = false;

            Data.NavMeshAgent.isStopped = true;

            _context = CreateContext();
            
            Data.BehaviourTreeInstance.Initialize(_context);

            OnInitialize();
            
            gameObject.SetActive(false);
        }

        protected override T Resolve<T>()
        {
            var resolveResult = base.Resolve<T>();
            
            if (resolveResult is IEnemyPoolPart enemyPoolPart) 
                _poolParts.Add(enemyPoolPart);

            return resolveResult;
        }
        
        private void OnUpdate()
        {
            Data.BehaviourTreeInstance.Execute();
        }

        private void OnIsDead(bool isDead)
        {
            if(!isDead)
                return;
            
            EnemyVisualBase.PlayDeathAnimation();
            
            _aliveDisposables?.Dispose();
            
            Data.NavMeshAgent.isStopped = true;
            Data.NavMeshAgent.ResetPath();
            
            _onDead.Execute(this);
        }

        public bool SetDestination(Vector3 position)
        {
            var result = Data.NavMeshAgent.SetDestination(position);
            return result;
        }
    }
}