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
        private IProjectilesSender _sender;
        
        private readonly ReactiveCommand<IPoolProjectile> _disappeared = new();

        public IObservable<IPoolProjectile> Disappeared => _disappeared;

        public void Appear(Vector2 direction, Vector2 position, IProjectilesSender sender)
        {
            _sender = sender;
            
            gameObject.SetActive(true);
            
            transform.position = position;

            mainRigidbody.isKinematic = false;
            mainRigidbody.velocity = direction * _projectileData.Speed;

            var negateDirection = direction * -1f;
            var angle = Mathf.Atan2(negateDirection.y, negateDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            _appearDisposables = new CompositeDisposable();

            triggerCollider.OnTriggerEnter2DAsObservable().Subscribe(OnTriggerColliderEnter)
                .AddTo(_appearDisposables, this);

            Observable.Timer(TimeSpan.FromSeconds(_projectileData.AutomaticDisappearDelaySeconds))
                .Subscribe(_ => Disappear()).AddTo(_appearDisposables, this);
        }

        private void OnTriggerColliderEnter(Collider2D other)
        {
            var enemyLayer = _layerMasksParameters.EnemyLayer;
            var isCollidingWithEnemy = IsInLayerMask(other, enemyLayer);

            if (_projectileData.IgnoreEnemies && isCollidingWithEnemy)
                return;
            
            if(other.TryGetComponent(out IProjectilesSender otherSender) && otherSender.Equals(_sender))
                return;

            Disappear();

            var playerLayer = _layerMasksParameters.PlayerLayer;
            var isCollidingWithPlayer = IsInLayerMask(other, playerLayer);
            
            if(!isCollidingWithEnemy && !isCollidingWithPlayer)
                return;

            if (!other.TryGetComponent(out IDamageable damageable))
                return;

            damageable.HandleDamage(_projectileData.Damage);
        }

        private void Disappear()
        {
            gameObject.SetActive(false);
            _appearDisposables?.Dispose();
            mainRigidbody.velocity = Vector2.zero;
            mainRigidbody.isKinematic = true;
        }
        
        bool IsInLayerMask(Collider2D obj, int layerMask)
        {
            var objLayerMask = 1 << obj.gameObject.layer;
            return (layerMask & objLayerMask) != 0;
        }
    }
}