using Unity.Entities;
using UnityEngine;


public class PrefabSpawnerBaker : MonoBehaviour 
{
    public GameObject prefabToSpawn;
    //public float cooldownTimer;
    public float cooldownAmount;
    public int maxSpawnLimit;

    class baker : Baker<PrefabSpawnerBaker>
    {
        public override void Bake(PrefabSpawnerBaker authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SpawnComponent
            {
                prefabToSpawn = GetEntity(authoring.prefabToSpawn, TransformUsageFlags.Dynamic),
                cooldownAmount = authoring.cooldownAmount,
                cooldownTimer = 0,
                maxSpawnLimit = authoring.maxSpawnLimit,
            });
            AddComponent<InputComp>(entity);
        }
    }
}

public struct SpawnComponent : IComponentData
{
    public Entity prefabToSpawn;
    public float cooldownTimer;
    public float cooldownAmount;
    public int maxSpawnLimit;
    public int numbOfSpawnedEntities;
}

public struct InputComp : IComponentData
{
    public bool spacePressed; 
}