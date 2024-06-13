namespace Db.Player
{
    public interface IPlayerParameters
    {
        public float MovementSpeed { get; }
        
        public float DashSpeed { get; }
        public float DashDurationSeconds { get; }
    }
}