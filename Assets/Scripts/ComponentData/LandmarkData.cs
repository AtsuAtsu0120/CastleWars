using Unity.Entities;
using Unity.Mathematics;

namespace ComponentData
{
    public struct LandmarkData : IComponentData
    {
        public float3 Position;
        public bool IsEnemy;
        public float ConquestPercentage;
    }

    public struct SelectLandmarkTag : IComponentData
    {
        
    }

    public struct SelectedLandmarkTag : IComponentData
    {
        
    }
}