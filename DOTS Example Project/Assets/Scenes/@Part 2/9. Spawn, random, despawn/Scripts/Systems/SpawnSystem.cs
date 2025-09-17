using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
public partial struct SpawnSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ECBSingletonComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        var ecbSingleton = SystemAPI.GetSingleton<ECBSingletonComponent>();
        
        if (ecbSingleton.SchedulingType == SchedulingType.Run)
        {
            int n = ecbSingleton.spawnAmount; 
            for (int i = 0; i < n*n*n ; i++)
            {
                    var e = state.EntityManager.Instantiate(ecbSingleton.prefabTospawn);
                    float x = (i % n) * 2f;
                    float y = ((i / n) % n) * 2f;
                    float z = (i / (n * n)) * 2f;
        
                    state.EntityManager.SetComponentData(e, LocalTransform.FromPosition(new float3(x, y, z)));
            }; 
        }
        else if (ecbSingleton.SchedulingType == SchedulingType.Schedule)
        {
            var ECB = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            state.Dependency = new spawnCubes
            {
                ecb = ECB
            }.Schedule(state.Dependency);
        }
        else if (ecbSingleton.SchedulingType == SchedulingType.ScheduleParallel)
        {
            var ECB = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            state.Dependency = new spawnCubesParallel
            {
                ecb = ECB
            }.ScheduleParallel(state.Dependency);
        }
        else if (ecbSingleton.SchedulingType == SchedulingType.ScheduleParallelEnable)
        {
            var ECB = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            state.Dependency = new spawnCubesParallel
            {
                ecb = ECB
            }.ScheduleParallel(state.Dependency);
        }
    }
}

[BurstCompile]
public partial struct spawnCubes : IJobEntity
{
    public EntityCommandBuffer ecb; 
    public void Execute(in ECBSingletonComponent ecbSingleton)
    {
        int n = ecbSingleton.spawnAmount; 
        for (int i = 0; i < n*n*n ; i++)
        {
                var e = ecb.Instantiate(ecbSingleton.prefabTospawn);
                float x = (i % n) * 2f;
                float y = ((i / n) % n) * 2f;
                float z = (i / (n * n)) * 2f;
                
                ecb.AddComponent(e, LocalTransform.FromPosition(new float3(x, y, z)));
        }
    }
}

[BurstCompile]
public partial struct spawnCubesParallel : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ecb; 
    public void Execute([ChunkIndexInQuery] int key, in ECBSingletonComponent ecbSingleton)
    {
        int n = ecbSingleton.spawnAmount; 
        for (int i = 0; i < n*n*n ; i++)
        {
            var e = ecb.Instantiate(key, ecbSingleton.prefabTospawn);
            float x = (i % n) * 2f;
            float y = ((i / n) % n) * 2f;
            float z = (i / (n * n)) * 2f;
                
            ecb.AddComponent(key,e, LocalTransform.FromPosition(new float3(x, y, z)));
        }
    }
}