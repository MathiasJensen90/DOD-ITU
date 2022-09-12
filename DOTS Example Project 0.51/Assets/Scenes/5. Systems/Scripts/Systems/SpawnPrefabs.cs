using System.Globalization;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.CodeGeneratedJobForEach;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class SpawnPrefabs : SystemBase
{
    public BeginInitializationEntityCommandBufferSystem ecbSystem; 
    protected override void OnCreate()
    {
        RequireSingletonForUpdate<SystemExampleSingleton>();
        ecbSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        float dt = Time.DeltaTime;

        Entities.ForEach((ref InputComp inputComp) =>
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                inputComp.spacePressed = !inputComp.spacePressed;
                Debug.Log("spawnstate changed");
            }
        
        }).Run();

        var spawnJob = new SpawnJob
        {
            dt = dt,
            ecb = ecb
        };
        spawnJob.Run();
        ecb.Playback(EntityManager);
        ecb.Dispose();
    }
}


public partial struct SpawnJob : IJobEntity 
{
    public float dt;
    public EntityCommandBuffer ecb;
    public void Execute(ref SpawmComponent spawnComp, in InputComp inputComp)
    {
        if (!inputComp.spacePressed || spawnComp.numbOfSpawnedEntities >= spawnComp.maxSpawnLimit) return;

        if (0 <= spawnComp.cooldownTimer )
        {
            spawnComp.cooldownTimer -= dt;
        }
        else
        {
            spawnComp.cooldownTimer = spawnComp.cooldownAmount;
            spawnComp.numbOfSpawnedEntities++;
            var prefabEntity = spawnComp.prefabToSpawn;
            ecb.SetComponent(prefabEntity, new Translation
            {
                Value = new float3(0, 0, 1.5f * spawnComp.numbOfSpawnedEntities)
            });
            ecb.Instantiate(prefabEntity);
        }
    }
}


