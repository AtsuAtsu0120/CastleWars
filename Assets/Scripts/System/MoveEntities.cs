using ComponentData;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace System
{
    public partial struct MoveEntities : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, data) in
                     SystemAPI.Query<RefRW<LocalTransform>, TrooperData>())
            {
                var diff = data.TargetLandmarkPosition - transform.ValueRO.Position;
                var normalizedDiff = math.normalize(diff);
                
                transform.ValueRW.Position += normalizedDiff;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}