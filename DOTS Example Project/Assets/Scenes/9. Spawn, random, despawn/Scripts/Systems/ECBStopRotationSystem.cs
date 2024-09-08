using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

[UpdateAfter(typeof(SpawnSystem))]
public partial struct ECBStopRotationSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ECBSingletonComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float elapsedTime = (float)SystemAPI.Time.ElapsedTime;
        float dt = SystemAPI.Time.DeltaTime;
        EntityCommandBuffer ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

        var job = new InitRandomJob
        {
            elapsedTime = elapsedTime
        }.Schedule(state.Dependency);
        
        var job2 =  new StopRotatingJob
        {
            elapsedTime = elapsedTime,
            ecb = ecb
        }.Schedule(job);
        
        job2.Complete();
       
        ecb.Playback(state.EntityManager);
    }
}

public partial struct InitRandomJob : IJobEntity
{
    public float elapsedTime;
    public void Execute([EntityIndexInQuery] int entityIndex,  ref RandomData randData)
    {
        if (!randData.dataInitiliazed)
        {
            randData.Value.InitState( (uint)entityIndex + 34);
            randData.dataInitiliazed = true;
            randData.timer += elapsedTime;
        }
    }
}


[WithNone(typeof(StopRotatingTag))]
public partial struct StopRotatingJob : IJobEntity
{
    public float elapsedTime;
    public EntityCommandBuffer ecb;
    public void Execute(Entity entity, ref RandomData randData, ref URPMaterialPropertyBaseColor baseCol)
    {
        float4 red = new float4(1, 0, 0, 1);
        float4 green = new float4(0, 1, 0, 1);
        float4 blue = new float4(0, 0, 1, 1);
        
        float timer = elapsedTime - randData.timer;
        baseCol.Value = math.lerp(blue, green, math.saturate(timer / randData.duration));
        if (timer < randData.duration) return;
        if (randData.Value.NextInt(0,100) % 2 == 0)
        {
            ecb.AddComponent(entity, new StopRotatingTag
            {
                timer = elapsedTime + 2 + randData.Value.NextFloat(1,3)
            });
            baseCol.Value = red;
        }
        else
        {
            randData.timer = elapsedTime; 
            baseCol.Value = blue; 
        }
    }
}

