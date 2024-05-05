using ComponentData;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace System
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public partial struct PlayerSystem : ISystem
    {
        private const float EnergyRate = 0.1f;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            AddEnergy(ref state);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }

        private void AddEnergy(ref SystemState state)
        {
            foreach (var player in SystemAPI.Query<RefRW<PlayerData>>())
            {
                player.ValueRW.Energy += EnergyRate;
            }
        }
    }
}