using Unity.Entities;
using UnityEngine;

class PickupTagBaker : MonoBehaviour
{
    class PickupTagBakerBaker : Baker<PickupTagBaker>
    {
        public override void Bake(PickupTagBaker authoring)
        {
            var e = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PickupTag>(e);
        }
    }
}

public struct PickupTag : IComponentData
{
    
}
