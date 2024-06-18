using Game.Level.View;
using Game.Level.View.Impl;

namespace Game.Level.Provider
{
    public interface ILevelViewProvider
    {
        LevelView LevelView { get; }
    }
}