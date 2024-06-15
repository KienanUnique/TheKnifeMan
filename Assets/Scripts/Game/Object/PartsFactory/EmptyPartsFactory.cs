using Zenject;

namespace Game.Object.PartsFactory
{
    public class EmptyPartsFactory : APartsFactory
    {
        public EmptyPartsFactory(DiContainer mainContainer) : base(mainContainer)
        {
        }

        protected override void HandleCreateParts(DiContainer container, object[] extraArgs)
        {
        }
    }
}