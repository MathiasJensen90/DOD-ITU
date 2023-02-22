using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


[DisableAutoCreation]
[BurstCompile]
public partial struct InteractionExperimentSystem : ISystem
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
        NativeArray<float3> distance = new NativeArray<float3>(3, Allocator.Temp);

        var dt = SystemAPI.Time.DeltaTime; 

        foreach (var (translation, e) in SystemAPI.Query<Translation>().WithAll<PlayerInteractionTag>().WithEntityAccess())
        {
            
            distance[0] = translation.Value + dt;
        }
        
        
        foreach (var translation in SystemAPI.Query<Translation>().WithAll<enemyInteractionTag>())
        {
            distance[1] = translation.Value;
        }

        var dist = math.distance(distance[0], distance[1]);
        
        Debug.DrawLine(distance[0], distance[1]);
        
        Debug.Log($"{dist}");

        distance.Dispose();
    }
}
