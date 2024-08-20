using Game.Object.Part;
using Game.Utils.Directions;

namespace Game.Player.Parts.Visual
{
    public interface IPlayerVisualPart : IObjectPart
    {
        void ChangeLookDirection(EDirection1D direction1D);
        void PlayInjuredAnimation();
        void PlayDeathAnimation();
        void PlayAttackAnimation(EDirection2D direction2D);
        void StartPlayingDashAnimation();
        void PlayBlinkAnimation(float durationSeconds);
    }
}