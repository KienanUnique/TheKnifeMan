using UnityEngine;
using Utils;

namespace Db.LayerMasks.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(LayerMasksParameters),
        fileName = nameof(LayerMasksParameters))]
    public class LayerMasksParameters : ScriptableObject, ILayerMasksParameters
    {
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private LayerMask projectilesLayer;

        public int PlayerLayer => playerLayer.value;
        public int EnemyLayer => enemyLayer.value;
        public int ProjectilesLayer => projectilesLayer.value;
    }
}