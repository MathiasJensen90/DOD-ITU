using Unity.Entities;
using UnityEngine;

public class TagComponent2Baker : MonoBehaviour
{

    class baker : Baker<TagComponent2Baker>
    {
        public override void Bake(TagComponent2Baker authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<TagComponent2>(entity);
        }
    }
}

public struct TagComponent2 : IComponentData
{
        
}