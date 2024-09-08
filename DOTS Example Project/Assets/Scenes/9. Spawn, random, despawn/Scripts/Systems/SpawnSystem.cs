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
        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        JobHandle spawnjob = new spawnCubes
        {
            ecb = ecb
        }.Schedule(state.Dependency);
        spawnjob.Complete();
        ecb.Playback(state.EntityManager);
    }
}

public partial struct spawnCubes : IJobEntity
{
    public EntityCommandBuffer ecb; 
    public void Execute(Entity entity, in ECBSingletonComponent ecbSingleton)
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