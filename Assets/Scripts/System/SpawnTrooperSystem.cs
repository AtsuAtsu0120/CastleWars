using ComponentData;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace System
{
    public partial struct SpawnTrooperSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            Spawn(ref state);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        private void Spawn(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (playerData, _, entity) in SystemAPI.Query<PlayerData, SpawnTrooperTag>().WithEntityAccess())
            {
                var trooper = ecb.CreateEntity();
                
                var trooperData = new TrooperData();
                trooperData.TargetLandmarkPosition = playerData.SelectLandmarkPosition;
                ecb.AddComponent(trooper, trooperData);
                
                var defaultPosition = new LocalTransform();
                defaultPosition.Position = playerData.SpawnPosition;
                ecb.AddComponent(trooper, defaultPosition);

                ecb.RemoveComponent<SpawnTrooperTag>(entity);
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}