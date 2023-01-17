using System.Globalization;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.CodeGeneratedJobForEach;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[DisableAutoCreation]
public partial struct SpawnPrefabs : ISystem
{
    //public BeginInitializationEntityCommandBufferSystem ecbSystem;

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SystemExampleSingleton>();
       // ecbSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    public void OnDestroy(ref SystemState state)
    {
       
    }

    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
        //var ecb = new EntityCommandBuffer(Allocator.TempJob);
        float dt = SystemAPI.Time.DeltaTime;

        // Entities.ForEach((ref InputComp inputComp) =>
        // {
        //     if (Input.GetKeyDown(KeyCode.Space))
        //     {
        //         inputComp.spacePressed = !inputComp.spacePressed;
        //         Debug.Log("spawnstate changed");
        //     }
        //
        // }).Run();

        foreach (var input in SystemAPI.Query<RefRW<InputComp>>())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                input.ValueRW.spacePressed = !input.ValueRW.spacePressed;
                Debug.Log("spawnstate changed");
            }
        }

        new SpawnJob
        {
            dt = dt,
            ecb = ecb
        }.Schedule(state.Dependency);
        
        ecb.Playback(state.EntityManager);
        
        //spawnJob.Run();
        // ecb.Playback(EntityManager);
        // ecb.Dispose();
    }
}


public partial struct SpawnJob : IJobEntity 
{
    public float dt;
    public EntityCommandBuffer ecb;
    public void Execute(ref SpawnComponent spawnComp, in InputComp inputComp)
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


