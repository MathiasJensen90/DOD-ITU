using System.Globalization;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.CodeGeneratedJobForEach;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct SpawnPrefabs : ISystem
{
    //public BeginInitializationEntityCommandBufferSystem ecbSystem;

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SystemExampleSingleton>();
    }

    public void OnDestroy(ref SystemState state)
    {
       
    }

    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
        float dt = SystemAPI.Time.DeltaTime;

        foreach (var input in SystemAPI.Query<RefRW<InputComp>>())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                input.ValueRW.spacePressed = !input.ValueRW.spacePressed;
                Debug.Log("spawnstate changed");
            }
        }

        JobHandle jobhandle = new SpawnJob
        {
            dt = dt,
            ecb = ecb
        }.Schedule(state.Dependency);
        jobhandle.Complete();
        ecb.Playback(state.EntityManager);
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
            ecb.SetComponent(prefabEntity, new LocalTransform
            {
                Position = new float3(0, 0, 1.5f * spawnComp.numbOfSpawnedEntities),
                Rotation = quaternion.identity,
                Scale = 1
            });
            ecb.Instantiate(prefabEntity);
        }
    }
}


