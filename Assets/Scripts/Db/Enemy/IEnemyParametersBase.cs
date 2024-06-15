namespace Db.Enemy
{
    public interface IEnemyParametersBase
    {
        // movement
        float Acceleration { get; }
        float MaxSpeed { get; }
        
        // character
        int Health { get; }
    }
}