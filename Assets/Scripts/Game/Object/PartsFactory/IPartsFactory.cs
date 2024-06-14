using Game.Object.Part;

namespace Game.Object.PartsFactory
{
    public interface IPartsFactory
    {
        void CreateParts(object[] extraArgs);
        T Resolve<T>() where T : IObjectPart;
    }
}