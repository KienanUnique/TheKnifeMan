namespace Db.Player
{
    public interface IPlayerParameters
    {
        // character
        int Health { get; }
        
        // movement
        float MovementSpeed { get; }
        float DashSpeed { get; }
        float DashDurationSeconds { get; }
        
        // animator
        float AnimatorMovingVelocityThreshold { get; }
        int LowHealthThreshold { get; }
    }
}