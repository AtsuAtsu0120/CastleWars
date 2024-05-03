using Unity.Entities;
using Unity.Mathematics;

namespace ComponentData
{
    public struct PlayerData : IComponentData
    {
        public PlayerData(float energy, float3 spawnPosition, float3 selectLandmarkPosition)
        {
            Energy = energy;
            SpawnPosition = spawnPosition;
            SelectLandmarkPosition = selectLandmarkPosition;
        }

        public float Energy { get; set; }
        public float3 SpawnPosition { get; set; }
        public float3 SelectLandmarkPosition { get; set; }
    }

    public struct SpawnTrooperTag : IComponentData
    {
        
    }
}