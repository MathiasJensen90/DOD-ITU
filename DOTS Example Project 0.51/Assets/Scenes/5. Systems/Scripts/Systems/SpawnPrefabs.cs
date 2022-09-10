using System.Globalization;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
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
        var ecb = ecbSystem.CreateCommandBuffer(); 
        float dt = Time.DeltaTime;

        Entities.ForEach((ref InputComp inputComp) =>
        {
            if (Input.GetKeyDown(KeyCode.Space)) inputComp.spacePressed = !inputComp.spacePressed;
        
        }).Run();

        // Entities.ForEach((Entity entity, ref SpawmComponent spawnComp, in InputComp inputComp) =>
        // {
        //     if (!inputComp.spacePressed || spawnComp.numbOfSpawnedEntities >= spawnComp.maxSpawnLimit) return;
        //
        //     if (0 <= spawnComp.cooldownTimer )
        //     {
        //         spawnComp.cooldownTimer -= dt;
        //     }
        //     else
        //     {
        //         spawnComp.cooldownTimer = spawnComp.cooldownAmount;
        //         spawnComp.numbOfSpawnedEntities++;
        //         var prefabEntity = spawnComp.prefabToSpawn;
        //         EntityManager.SetComponentData(prefabEntity, new Translation
        //         {
        //             Value = new float3(0, 0, 1.5f * spawnComp.numbOfSpawnedEntities)
        //         });
        //
        //         EntityManager.Instantiate(prefabEntity);
        //     }
        // }).WithStructuralChanges().Run();

        var spawnJob = new SpawnJob
        {
            dt = dt,
            ecb = ecb
        };
        spawnJob.Run();

    }
}


public partial struct SpawnJob : IJobEntity
{
    public float dt;
    public EntityCommandBuffer ecb; 
    public void Execute(Entity entity, ref SpawmComponent spawnComp, in InputComp inputComp)
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


