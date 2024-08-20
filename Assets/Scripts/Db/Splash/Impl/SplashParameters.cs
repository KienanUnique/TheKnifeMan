using UnityEngine;
using Utils;

namespace Db.Splash.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(SplashParameters),
        fileName = nameof(SplashParameters))]
    public class SplashParameters : ScriptableObject, ISplashParameters
    {
        [SerializeField] private float splashDuration;
        [SerializeField] private float closeLogoDelay;

        public float SplashDuration => splashDuration;
        public float CloseLogoDelay => closeLogoDelay;
    }
}