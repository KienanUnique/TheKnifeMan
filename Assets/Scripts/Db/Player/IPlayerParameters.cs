namespace Db.Player
{
    public interface IPlayerParameters
    {
        // character
        int Health { get; }
        int Damage { get; }

        // movement
        float MovementSpeed { get; }
        float DashForce { get; }
        float DashDurationSeconds { get; }
        float DashCooldownSeconds { get; }
        float DashDrag { get; }

        // animator
        float AnimatorMovingVelocityThreshold { get; }
        int LowHealthThreshold { get; }
        float DashEndAnimationDurationSeconds { get; }
    }
}