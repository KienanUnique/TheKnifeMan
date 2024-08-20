using Game.Level.View;
using Game.Level.View.Impl;

namespace Game.Level.Provider.Impl
{
    public class LevelViewProvider : ILevelViewProvider
    {
        private readonly LevelView _levelView;

        public LevelViewProvider(LevelView levelView)
        {
            _levelView = levelView;
        }

        public LevelView LevelView => _levelView;
    }
}