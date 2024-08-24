using Unity.Entities;
using UnityEngine;

public class TagComponent1Baker : MonoBehaviour
{

    class baker : Baker<TagComponent1Baker>
    {
        public override void Bake(TagComponent1Baker authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<TagComponent1>(entity);
        }
    }
}

public struct TagComponent1 : IComponentData
{
    
}