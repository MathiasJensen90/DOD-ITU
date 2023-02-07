using Unity.Entities;
using UnityEngine;

public class BufferOfEntitiesCreatedBaker : MonoBehaviour
{
    
    class baker : Baker<BufferOfEntitiesCreatedBaker>
    {
        public override void Bake(BufferOfEntitiesCreatedBaker authoring)
        {
            AddBuffer<ListOfEntitiesCreatedComponent>();
        }
    }
}

public struct ListOfEntitiesCreatedComponent : IBufferElementData
{
    public Entity entity; 
}


