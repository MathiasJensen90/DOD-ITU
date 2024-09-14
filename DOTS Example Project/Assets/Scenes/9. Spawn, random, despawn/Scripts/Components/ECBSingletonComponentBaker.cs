using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


public class ECBSingletonComponentBaker : MonoBehaviour
{
    public SchedulingType SchedulingType;
    [Range(1, 200)]
    public int spawnAmount;
    public GameObject prefabToSpawn;
    class baker : Baker<ECBSingletonComponentBaker>
    {
        public override void Bake(ECBSingletonComponentBaker authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ECBSingletonComponent
            {
                SchedulingType = authoring.SchedulingType,
                spawnAmount = authoring.spawnAmount,
                prefabTospawn = GetEntity(authoring.prefabToSpawn, TransformUsageFlags.Dynamic) 
            });
        }
    }
}




public struct ECBSingletonComponent : IComponentData
{
    public SchedulingType SchedulingType;
    public int spawnAmount;
    public Entity prefabTospawn;
}


public enum SchedulingType{
    Run,
    Schedule
}