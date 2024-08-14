using Alchemy.Inspector;
using Game.Enemy;
using Game.Object;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Game.Utils
{
    [CreateAssetMenu(menuName = MenuPathBase.Helpers + nameof(EnemiesCollidersSetup),
        fileName = nameof(EnemiesCollidersSetup))]
    public class EnemiesCollidersSetup : ScriptableObject
    {
        private const string SEARCH_FOLDER = "Assets/";
        private const float TREASHOLD = 0.01f;

        [Header("Collider")]
        [SerializeField] private Vector2 colliderSize = new(0.5f, 0.25f);
        
        [Header("Rigidbody")]
        [SerializeField] private float mass = 1; 
        [SerializeField] private float linearDrag = 0f;
        [SerializeField] private float angularDrag = 0.05f;

#if UNITY_EDITOR
        [Button]
        private void UpdateValuesInPrefabs()
        {
            var assets = AssetDatabase.FindAssets("t:prefab", new[] {SEARCH_FOLDER});

            foreach (var asset in assets)
            {
                var path = AssetDatabase.GUIDToAssetPath(asset);
                var controllerBase = AssetDatabase.LoadAssetAtPath<AObjectControllerBase>(path);

                if (controllerBase is IPoolEnemy)
                {
                    var rigidbodyToSetup = controllerBase.TryGetComponent(out Rigidbody2D existingRigidbody)
                        ? existingRigidbody
                        : controllerBase.AddComponent<Rigidbody2D>();

                    rigidbodyToSetup.gravityScale = 0f;
                    rigidbodyToSetup.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                    rigidbodyToSetup.freezeRotation = true;
                    rigidbodyToSetup.mass = mass;
                    rigidbodyToSetup.angularDrag = angularDrag;
                    rigidbodyToSetup.drag = linearDrag;

                    var boxCollider = controllerBase.GetComponent<BoxCollider2D>();
                    var deltaSize = boxCollider.size - colliderSize;

                    if (deltaSize.sqrMagnitude > TREASHOLD)
                    {
                        var deltaSizeY = deltaSize.y;
                        var newOffset = new Vector2(0f, boxCollider.offset.y - deltaSizeY / 2);

                        boxCollider.size = colliderSize;
                        boxCollider.offset = newOffset;
                    }

                    Debug.Log($"Colliders changed in {controllerBase.name} prefab.");

                    EditorUtility.SetDirty(controllerBase);
                    AssetDatabase.SaveAssetIfDirty(controllerBase);
                }
            }
        }
#endif
    }
}