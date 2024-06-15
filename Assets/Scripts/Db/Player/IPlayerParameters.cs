namespace Db.Player
{
    public interface IPlayerParameters
    {
        // character
        public int Health { get; }
        
        // movement
        public float MovementSpeed { get; }
        public float DashSpeed { get; }
        public float DashDurationSeconds { get; }
        
        // animator
        public float AnimatorMovingVelocityThreshold { get; }
        public int LowHealthThreshold { get; }
    }
}