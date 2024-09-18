using Unity.Entities;
using UnityEngine;


class MoveDataAuthoring : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    class baker : Baker<MoveDataAuthoring>
    {
        public override void Bake(MoveDataAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new moveData
            {
                moveSpeed = authoring.moveSpeed,
                rotationSpeed = authoring.rotationSpeed,
            });
        }
    }
}


public struct moveData : IComponentData
{
    public float moveSpeed;
    public float rotationSpeed;
}