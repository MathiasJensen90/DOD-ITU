using Unity.Entities;
using UnityEngine;

public class EntityManagerSingletonComponentBaker : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public bool hasBuffer;
    public EntityManagerExample ExampleType;

    class baker : Baker<EntityManagerSingletonComponentBaker>
    {
        public override void Bake(EntityManagerSingletonComponentBaker authoring)
        {
            AddComponent(new ManagerSingeltonComponent
            {
                prefabToSpawn = GetEntity(authoring.prefabToSpawn),
                hasBuffer = authoring.hasBuffer,
                ExampleType = authoring.ExampleType
            });

        }
    }
}


public struct ManagerSingeltonComponent : IComponentData
{
    public Entity prefabToSpawn;
    public bool hasBuffer;
    public EntityManagerExample ExampleType;
} 


public enum EntityManagerExample{
    Simple,
    Complex
}
