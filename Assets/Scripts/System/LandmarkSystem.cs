using ComponentData;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace System
{
    public partial struct LandmarkSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            SelectLandmark(ref state);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        private void SelectLandmark(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (_, landmarkData, entity) in SystemAPI.Query<SelectLandmarkTag, LandmarkData>().WithEntityAccess())
            {
                ecb.RemoveComponent<SelectLandmarkTag>(entity);
                var playerData = SystemAPI.GetSingletonRW<PlayerData>();

                playerData.ValueRW.SelectLandmarkPosition = landmarkData.Position;
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}