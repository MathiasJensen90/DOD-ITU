using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

 
public partial struct WaveMoveSystem : ISystem
{
    [BurstCompile]  
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SystemExampleSingleton>();
    }

    public void OnDestroy(ref SystemState state)
    {
       
    }
    [BurstCompile]  
    public void OnUpdate(ref SystemState state)
    {
        var elapsedTime = (float)SystemAPI.Time.ElapsedTime;
        //RefRW - read and write data
        // RefRO - Readonly data
        foreach (var (trans, waveData) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<SinWaveComponent>>())
        {
            var waveMovement = waveData.ValueRO.amplitude * math.sin(elapsedTime * waveData.ValueRO.frequency);
            trans.ValueRW.Position = new float3(trans.ValueRO.Position.x, waveMovement, trans.ValueRO.Position.z);
        }
        
       
        // new SinMovementJob
        // {
        //     elapsedTime = elapsedTime
        // }.Schedule();
    }
}
//
// [BurstCompile]  
// public partial struct SinMovementJob : IJobEntity
// {
//     public float elapsedTime;
//     public void Execute(ref LocalTransform trans, in SinWaveComponent waveData)
//     {
//         var waveMovement = waveData.amplitude * math.sin(elapsedTime * waveData.frequency);
//         trans.Position = new float3(trans.Position.x, waveMovement, trans.Position.z);
//     }
// }

