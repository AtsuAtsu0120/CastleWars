using Unity.Entities;
using Unity.Mathematics;

namespace ComponentData
{
    public struct TrooperData : IComponentData
    {
        public float3 TargetLandmarkPosition;
    }
}
