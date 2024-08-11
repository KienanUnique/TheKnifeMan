namespace Db.Sounds
{
    public interface IGameDefaultParameters
    {
        float SoundsVolume { get; }
        float MusicVolume { get; }
        bool IsEasyModeEnabled { get; }
    }
}