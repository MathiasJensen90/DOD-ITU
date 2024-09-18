using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;



public partial struct BuiltinURPColorSystemg : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ShaderSingletonComp>();
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float elapsedTime = (float)SystemAPI.Time.ElapsedTime;

        new AffectBuiltInURPProperties
        {
            elapsedTime =  elapsedTime
        }.Schedule();
    }
}

[BurstCompile]
public partial struct AffectBuiltInURPProperties : IJobEntity
{
    public float elapsedTime; 
    
    public void Execute(ref URPMaterialPropertyBaseColor baseColor)
    {
        float3 rgb; 
        rgb.x = math.saturate(math.sin(elapsedTime + 1));
        rgb.y = math.saturate(math.cos(elapsedTime + 5));
        rgb.z = math.saturate(math.sin(elapsedTime + 3));

        baseColor.Value = new float4(rgb, 1);
    }
}
