using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace ComponentData
{
    public struct TrooperData : IComponentData
    {
        public float3 TargetLandmarkPosition { get; set; }
    }

    public struct TrooperDataWrapper : IComponentData
    {
        public TrooperDataWrapper(Entity entity)
        {
            Entity = entity;
        }

        public Entity Entity { get; set; }
    }
}
