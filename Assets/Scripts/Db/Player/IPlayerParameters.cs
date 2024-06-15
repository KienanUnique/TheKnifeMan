namespace Db.Player
{
    public interface IPlayerParameters
    {
        // movement
        public float MovementSpeed { get; }
        public float DashSpeed { get; }
        public float DashDurationSeconds { get; }
        
        // animator
        public float AnimatorMovingVelocityThreshold { get; }
    }
}