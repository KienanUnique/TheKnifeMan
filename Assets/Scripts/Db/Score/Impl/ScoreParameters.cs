using UnityEngine;
using Utils;

namespace Db.Score.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(ScoreParameters),
        fileName = nameof(ScoreParameters))]
    public class ScoreParameters : ScriptableObject, IScoreParameters
    {
        [SerializeField] private float additionalRatioForEnemyKill = 0.1f;

        public float AdditionalRatioForEnemyKill => additionalRatioForEnemyKill;
    }
}