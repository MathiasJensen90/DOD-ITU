using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public partial struct ChangeColorSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ShaderSingletonComp>();
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float dt = SystemAPI.Time.DeltaTime;
        float elapsedTime = (float)SystemAPI.Time.ElapsedTime;
        var spacePressed = Input.GetKeyDown(KeyCode.Space);

         new ColorLerpJob
        {
            
            dt = dt,
            elapsedTime = elapsedTime,
            spacePressed = spacePressed
        }.Schedule();

        new disolveJob
        {
            elapsedTime = elapsedTime
        }.Schedule();
    }
}


[BurstCompile]
public partial struct ColorLerpJob : IJobEntity
{
    public float dt;
    public float elapsedTime;
    public bool spacePressed; 
    
    public void Execute(ref MyMaterial color, ref DestinationColor destColor)
    {
        if (spacePressed)
        {
            color.Value = destColor.damageColor;
            destColor.lerpvalue = 0; 
        }

        destColor.lerpvalue = math.saturate(destColor.lerpvalue + elapsedTime * 0.001f); 
        var colorLerp = math.lerp(color.Value, destColor.startingColor, destColor.lerpvalue );
        color.Value = colorLerp; 
    }
}

[BurstCompile]
public partial struct disolveJob : IJobEntity
{
    public float elapsedTime; 
    
    public void Execute(ref DisolveComp disolveComp)
    {
        var disolveLerp = math.remap(-1, 1, 0, 1, math.sin(elapsedTime));
        disolveComp.Value = disolveLerp;
    }
}
