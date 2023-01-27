using Unity.Entities;
using UnityEngine;


public class PrefabSpawnerBaker : MonoBehaviour 
{
    public GameObject prefabToSpawn;
    public float cooldownTimer;
    public float cooldownAmount;
    public int maxSpawnLimit;

    class baker : Baker<PrefabSpawnerBaker>
    {
        public override void Bake(PrefabSpawnerBaker authoring)
        {
            AddComponent(new SpawnComponent
            {
                prefabToSpawn = GetEntity(authoring.prefabToSpawn),
                cooldownAmount = authoring.cooldownAmount,
                cooldownTimer = authoring.cooldownTimer,
                maxSpawnLimit = authoring.maxSpawnLimit,
            });
            AddComponent<InputComp>();
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