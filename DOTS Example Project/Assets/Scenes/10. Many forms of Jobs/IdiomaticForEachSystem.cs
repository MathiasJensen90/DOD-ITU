using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;



[BurstCompile]
public partial struct IdiomaticForEachSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<enemyInteractionTag>();
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        NativeArray<float3> distance = new NativeArray<float3>(3, Allocator.Temp);

        var dt = SystemAPI.Time.DeltaTime; 

        foreach (var (translation, e) in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<PlayerInteractionTag>().WithEntityAccess())
        {
            
            distance[0] = translation.ValueRO.Position + dt;
        }
        
        foreach (var translation in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<enemyInteractionTag>())
        {
            distance[1] = translation.ValueRO.Position;
        }

        var dist = math.distance(distance[0], distance[1]);
        Color col;
        if (dist < 7f)
        {
            col = Color.red;
        }
        else
        {
            col = Color.blue;
        }
        
        Debug.DrawLine(distance[0], distance[1], col);
        Debug.Log($"{dist}");
        distance.Dispose();
    }
}
