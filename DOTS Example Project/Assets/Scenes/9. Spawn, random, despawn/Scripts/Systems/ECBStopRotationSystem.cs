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
        EntityCommandBuffer ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
        var ecbSingleton = SystemAPI.GetSingleton<ECBSingletonComponent>();
        
        if (ecbSingleton.SchedulingType == SchedulingType.Run)
        {
            foreach (var (randData, entity) in SystemAPI.Query<RefRW<RandomData>>().WithEntityAccess())
            {
                if (!randData.ValueRO.dataInitiliazed)
                {
                    randData.ValueRW.Value.InitState( (uint)entity.Index + 34);
                    randData.ValueRW.dataInitiliazed = true;
                    randData.ValueRW.timer += elapsedTime;
                }
            }
            
            float4 red = new float4(1, 0, 0, 1);
            float4 green = new float4(0, 1, 0, 1);
            float4 blue = new float4(0, 0, 1, 1);
            foreach (var (randData,
                         baseCol,
                         entity) in SystemAPI.Query<RefRW<RandomData>,
                         RefRW<URPMaterialPropertyBaseColor>>().WithEntityAccess().WithNone<StopRotatingTag>())
            {
                float timer = elapsedTime - randData.ValueRO.timer;
                baseCol.ValueRW.Value = math.lerp(blue, green, math.saturate(timer / randData.ValueRO.duration));
                if (timer < randData.ValueRO.duration) continue;
                if (Random.Range(0,100) % 2 == 0)
                {
                    ecb.AddComponent(entity, new StopRotatingTag
                    {
                        timer = elapsedTime + 2 + Random.Range(1f,3f)  
                    });
                    baseCol.ValueRW.Value = red;
                }
                else
                {
                    randData.ValueRW.timer = elapsedTime; 
                    baseCol.ValueRW.Value = blue; 
                }
            }
        }
        else if (ecbSingleton.SchedulingType == SchedulingType.Schedule)
        {
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
        }
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

