using Game.Utils;
using UnityEngine;

namespace Db.Vfx
{
    public interface IVfxBase
    {
        ParticleSystem GetVfx(EVfxType type);
    }
}