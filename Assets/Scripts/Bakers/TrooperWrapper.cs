using System.Collections.Generic;
using ComponentData;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Bakers
{
    public class TrooperWrapper : MonoBehaviour
    {
        [SerializeField] private Trooper _trooper;
        public class TroopersListBaker : Baker<TrooperWrapper>
        {
            public override void Bake(TrooperWrapper authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new TrooperDataWrapper(GetEntity(authoring._trooper, TransformUsageFlags.Dynamic)));
            }
        }
    }
}