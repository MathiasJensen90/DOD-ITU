using Unity.Entities;
using UnityEngine;

public class TagComponent1Authoring : MonoBehaviour
{

    class baker : Baker<TagComponent1Authoring>
    {
        public override void Bake(TagComponent1Authoring authoring)
        {
            AddComponent<TagComponent1>();
        }
    }
}
public struct TagComponent1 : IComponentData
{
    
}
