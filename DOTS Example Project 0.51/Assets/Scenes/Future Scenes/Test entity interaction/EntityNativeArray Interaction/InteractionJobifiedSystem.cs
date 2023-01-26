using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct InteractionJobifiedSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        
    }

    public void OnDestroy(ref SystemState state)
    {
       
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //how to best pass data along entiites? 
        NativeArray<float3> distance = new NativeArray<float3>(3, Allocator.TempJob);
        
        new EnemyWritePosJob
        {
            enemyPos = distance
        }.Schedule();

        new DistJobExample
        {
            enemyPos = distance
        }.Schedule();

        state.Dependency.Complete();
        
        Debug.DrawLine(distance[0], distance[1]);
        
    }
}


[WithAll(typeof(enemyInteractionTag))]
public partial struct EnemyWritePosJob : IJobEntity
{
    public NativeArray<float3> enemyPos; 
    public void Execute(in Translation trans)
    {
        enemyPos[0] = trans.Value;
    }
}

[BurstCompile]
[WithAll(typeof(PlayerInteractionTag))]
public partial struct DistJobExample : IJobEntity
{
    public NativeArray<float3> enemyPos; 
    public void Execute(in Translation trans)
    {
        enemyPos[1] = trans.Value;
        var dist = math.distance(trans.Value, enemyPos[0]);
        Debug.Log($"{dist}");
    }
}