using ComponentData;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Bakers
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _energy;
        [SerializeField] private float3 _spawnPosition;
        [SerializeField] private float3 _selectLandmarkPosition;
        public class PlayerBaker : Baker<Player>
        {
            public override void Bake(Player authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new PlayerData
                    (authoring._energy, authoring._spawnPosition, authoring._selectLandmarkPosition));
            }
        }
    }
}