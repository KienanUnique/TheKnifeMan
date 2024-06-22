using System;
using Db.LayerMasks;
using Game.Interfaces;
using Game.Projectile.Data;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Game.Projectile.Controller.Impl
{
    public class ProjectileController : MonoBehaviour, IPoolProjectile
    {
        [SerializeField] private Rigidbody2D mainRigidbody;
        [SerializeField] private Collider2D triggerCollider;

        [Inject] private ILayerMasksParameters _layerMasksParameters;
        [Inject] private ProjectileData _projectileData;

        private CompositeDisposable _appearDisposables;

        private ReactiveCommand<IPoolProjectile> _disappeared;

        public IObservable<IPoolProjectile> Disappeared => _disappeared;

        public void Appear(Vector2 direction, Vector2 position)
        {
            gameObject.SetActive(true);
            mainRigidbody.velocity = direction * _projectileData.Speed;

            _appearDisposables = new CompositeDisposable();

            triggerCollider.OnTriggerEnterAsObservable().Subscribe(OnTriggerColliderEnter)
                .AddTo(_appearDisposables, this);

            Observable.Timer(TimeSpan.FromSeconds(_projectileData.AutomaticDisappearDelaySeconds))
                .Subscribe(_ => Disappear()).AddTo(_appearDisposables, this);
        }

        private void OnTriggerColliderEnter(Collider obj)
        {
            Disappear();

            var layerBitMask = 1 << obj.gameObject.layer;

            var playerLayer = _layerMasksParameters.PlayerLayer;
            var isCollidingWithPlayer = playerLayer == (playerLayer | layerBitMask);

            var enemyLayer = _layerMasksParameters.EnemyLayer;
            var isCollidingWithEnemy = enemyLayer == (enemyLayer | layerBitMask);

            if (!isCollidingWithPlayer && (!_projectileData.CanDamageEnemies || !isCollidingWithEnemy))
                return;

            if (!obj.TryGetComponent(out IDamageable damageable))
                return;

            damageable.HandleDamage(_projectileData.Damage);
        }

        private void Disappear()
        {
            gameObject.SetActive(false);
            _appearDisposables?.Dispose();
        }
    }
}