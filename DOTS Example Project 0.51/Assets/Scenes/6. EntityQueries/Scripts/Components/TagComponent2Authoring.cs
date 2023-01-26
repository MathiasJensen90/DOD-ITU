using Unity.Entities;
using UnityEngine;

public class TagComponent2Authoring : MonoBehaviour
{

    class Baker : Baker<TagComponent2Authoring>
    {
        public override void Bake(TagComponent2Authoring authoring)
        {
            AddComponent<TagComponent2>();
        }
    }
}
public struct TagComponent2 : IComponentData
{
    
}
