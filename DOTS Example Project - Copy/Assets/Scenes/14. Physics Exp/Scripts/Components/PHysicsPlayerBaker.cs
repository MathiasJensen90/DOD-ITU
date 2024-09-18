using Unity.Entities;
using UnityEngine;

class PHysicsPlayerBaker : MonoBehaviour
{
    public float speed;
    class PhBaker : Baker<PHysicsPlayerBaker>
    {
        public override void Bake(PHysicsPlayerBaker authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new physicsPlayerComp
            {
                speed = authoring.speed,
            });
        }
    }

}

public struct physicsPlayerComp : IComponentData
{
    public float speed;
}