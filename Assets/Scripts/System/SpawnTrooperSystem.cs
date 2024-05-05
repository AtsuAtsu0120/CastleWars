using ComponentData;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace System
{
    public partial struct SpawnTrooperSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TrooperDataWrapper>();
        }

        // [BurstCompile]
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
                var trooperDataWrapper = SystemAPI.GetSingleton<TrooperDataWrapper>();
                var trooper = ecb.Instantiate(trooperDataWrapper.Entity);
                
                var trooperData = new TrooperData();
                trooperData.TargetLandmarkPosition = playerData.SelectLandmarkPosition;
                ecb.SetComponent(trooper, trooperData);
                
                var defaultPosition = new LocalTransform();
                defaultPosition.Position = playerData.SpawnPosition;
                ecb.SetComponent(trooper, defaultPosition);

                ecb.RemoveComponent<SpawnTrooperTag>(entity);
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}