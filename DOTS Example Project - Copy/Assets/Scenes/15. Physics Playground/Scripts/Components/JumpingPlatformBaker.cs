using Unity.Entities;
using UnityEngine;

class JumpingPlatformBaker : MonoBehaviour
{
    public float force;
    class JumpingPlatformBakerBaker : Baker<JumpingPlatformBaker>
    {
        public override void Bake(JumpingPlatformBaker authoring)
        {
            var e = GetEntity(TransformUsageFlags.Renderable);
            AddComponent(e, new JumpingPlatform
            {
                force = authoring.force
            });
        }
    }
}

public struct JumpingPlatform : IComponentData
{
    public float force; 
}