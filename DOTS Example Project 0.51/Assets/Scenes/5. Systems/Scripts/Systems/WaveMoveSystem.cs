using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct WaveMoveSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SystemExampleSingleton>();
    }

    public void OnDestroy(ref SystemState state)
    {
       
    }

    public void OnUpdate(ref SystemState state)
    {
        var elapsedTime = (float)SystemAPI.Time.ElapsedTime;
        
        new SinMovementJob
        {
            elapsedTime = elapsedTime
        }.Schedule();
    }
}

public partial struct SinMovementJob : IJobEntity
{
    public float elapsedTime;
    public void Execute(ref Translation trans, in SinWaveComponent waveData)
    {
        var waveMovement = waveData.amplitude * math.sin(elapsedTime * waveData.frequency);
        trans.Value = new float3(trans.Value.x, waveMovement, trans.Value.z);
    }
}

