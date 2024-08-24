using Unity.Entities;
using UnityEngine;

public class TagComponent3Baker : MonoBehaviour
{

    class baker : Baker<TagComponent3Baker>
    {
        public override void Bake(TagComponent3Baker authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<TagComponent3>(entity);
        }
    }
}

public struct TagComponent3 : IComponentData
{
    
}