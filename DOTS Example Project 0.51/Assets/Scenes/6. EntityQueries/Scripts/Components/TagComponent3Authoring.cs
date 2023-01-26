using Unity.Entities;
using UnityEngine;

public class TagComponent3Authoring : MonoBehaviour
{

    class Baker : Baker<TagComponent3Authoring>
    {
        public override void Bake(TagComponent3Authoring authoring)
        {
            AddComponent<TagComponent3>();
        }
    }
}
public struct TagComponent3 : IComponentData
{
    
}
