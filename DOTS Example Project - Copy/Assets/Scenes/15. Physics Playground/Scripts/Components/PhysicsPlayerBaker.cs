using Unity.Entities;
using UnityEngine;

class PhysicsPlayerBaker : MonoBehaviour
{
    public float moveSpeed;
    class PhysicsPlayerBakerBaker : Baker<PhysicsPlayerBaker>
    {
        public override void Bake(PhysicsPlayerBaker authoring)
        {
            var e = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(e, new PhysicsPlayer
            {
                moveSpeed = authoring.moveSpeed,
                pickupAquirred = 0
            });
        }
    }
}

public struct PhysicsPlayer : IComponentData
{
    public float moveSpeed;
    public int pickupAquirred;
}
