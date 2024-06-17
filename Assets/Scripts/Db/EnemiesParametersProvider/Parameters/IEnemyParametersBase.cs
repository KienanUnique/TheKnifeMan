namespace Db.EnemiesParametersProvider.Parameters
{
    public interface IEnemyParametersBase
    {
        // movement
        float Acceleration { get; }
        float MaxSpeed { get; }
        
        // character
        int Health { get; }
        
        // visual
        float AnimatorMovingVelocityThreshold { get; }
    }
}