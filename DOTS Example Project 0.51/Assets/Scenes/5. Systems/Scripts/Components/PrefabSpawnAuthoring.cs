using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
public class PrefabSpawnAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public GameObject prefabToSpawn;
    public float cooldownTimer;
    public float cooldownAmount;
    public int maxSpawnLimit;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new SpawmComponent
        {
            prefabToSpawn = conversionSystem.GetPrimaryEntity(prefabToSpawn),
            cooldownTimer = cooldownTimer,
            maxSpawnLimit = maxSpawnLimit,
            cooldownAmount = cooldownAmount
        });
        dstManager.AddComponent<InputComp>(entity);
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(prefabToSpawn);
    }
}


public struct SpawmComponent : IComponentData
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
