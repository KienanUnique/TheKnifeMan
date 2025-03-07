namespace Db.EnemiesParameters.Parameters
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
        int PointsForKill { get; }
    }
}