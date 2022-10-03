using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

public partial class BuiltinURPColorSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireSingletonForUpdate<DisolveComp>();
    }

    protected override void OnUpdate()
    {
        float elapsedTime = (float)Time.ElapsedTime;

        var aBURPP = new AffectBuiltInURPProperties
        {
            elapsedTime =  elapsedTime
        }.Schedule();
    }
}


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
