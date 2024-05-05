using ComponentData;
using Unity.Entities;
using UnityEngine;

namespace Bakers
{
    public class Trooper : MonoBehaviour
    {
        public class TrooperBaker : Baker<Trooper>
        {
            public override void Bake(Trooper authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<TrooperData>(entity);
            }
        }
    }
}