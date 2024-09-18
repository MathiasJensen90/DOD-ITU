using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[DisableAutoCreation]
[BurstCompile]
public partial struct IJobEntityExampleSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<enemyInteractionTag>();
    }

    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        NativeArray<float3> distance = new NativeArray<float3>(3, Allocator.TempJob);
        
        var handle = new EnemyWritePosJob
        {
            enemyPos = distance
        }.Schedule(state.Dependency);

        var otherHandle = new DistJobExample
        {
            enemyPos = distance
        }.Schedule(handle);
        otherHandle.Complete();
        
        Color col;
        if (math.distance(distance[0], distance[1]) < 7f)
        {
            col = Color.red;
        }
        else
        {
            col = Color.blue;
        }
        Debug.DrawLine(distance[0], distance[1], col);
        distance.Dispose();
    }
}


[WithAll(typeof(enemyInteractionTag))]
public partial struct EnemyWritePosJob : IJobEntity
{
    public NativeArray<float3> enemyPos; 
    public void Execute(in LocalTransform trans)
    {
        enemyPos[0] = trans.Position;
    }
}

[BurstCompile]
[WithAll(typeof(PlayerInteractionTag))]
public partial struct DistJobExample : IJobEntity
{
    public NativeArray<float3> enemyPos; 
    public void Execute(in LocalTransform trans)
    {
        enemyPos[1] = trans.Position;
    }
}