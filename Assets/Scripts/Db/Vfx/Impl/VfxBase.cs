using System;
using System.Collections.Generic;
using Alchemy.Serialization;
using Game.Utils;
using UnityEngine;
using Utils;

namespace Db.Vfx.Impl
{
    [AlchemySerialize]
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(VfxBase), fileName = nameof(VfxBase))]
    public partial class VfxBase : ScriptableObject, IVfxBase
    {
        [AlchemySerializeField, NonSerialized] private Dictionary<EVfxType, ParticleSystem> _prefabs;
        [SerializeField] private float changeMeToApplyChanges;

        public ParticleSystem GetVfx(EVfxType type) => _prefabs[type];
    }
}