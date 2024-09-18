using Unity.Entities;
using UnityEngine;

class RaycastBaker : MonoBehaviour
{
    public float rayLength = 5f;
    class RaycastBakerBaker : Baker<RaycastBaker>
    {
        public override void Bake(RaycastBaker authoring)
        {
            var e = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(e, new Raycaster
            {
                rayLengt = authoring.rayLength
            });

        }
    }

}

public struct Raycaster : IComponentData
{
    public float rayLengt;
    // public PhysicsCategoryTags collidesWith;
    // public PhysicsCategoryTags belongsTo;
}