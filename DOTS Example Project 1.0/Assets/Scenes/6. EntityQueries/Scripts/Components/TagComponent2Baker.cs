using Unity.Entities;
using UnityEngine;

public class TagComponent2Baker : MonoBehaviour
{

    class baker : Baker<TagComponent2Baker>
    {
        public override void Bake(TagComponent2Baker authoring)
        {
            AddComponent<TagComponent2>();
        }
    }
}

public struct TagComponent2 : IComponentData
{
        
}