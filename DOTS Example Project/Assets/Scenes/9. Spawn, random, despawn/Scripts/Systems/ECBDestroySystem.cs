using Unity.Burst;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;


[UpdateAfter(typeof(ECBStopRotationSystem))]
public partial struct ECBDestroySystem : ISystem
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
        var ecbSingleton = SystemAPI.GetSingleton<ECBSingletonComponent>();
       
        if (ecbSingleton.SchedulingType == SchedulingType.Run)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            foreach (var (stopRotTag,
                         entity)  in SystemAPI.Query<RefRO<StopRotatingTag>>().WithEntityAccess())
            {
                if (elapsedTime >= stopRotTag.ValueRO.timer)
                {
                    ecb.DestroyEntity(entity);
                }
            }
        }
        else if (ecbSingleton.SchedulingType == SchedulingType.Schedule)
        {
            var ECB = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            
            new DestroyStoppedEntities
            {
                ECB = ECB,
                elapsedTime = elapsedTime
            }.Schedule();
        }
        else if (ecbSingleton.SchedulingType == SchedulingType.ScheduleParallel)
        {
            var ECB = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            
            new DestroyStoppedEntities
            {
                ECB = ECB,
                elapsedTime = elapsedTime
            }.ScheduleParallel();
        }
        else if (ecbSingleton.SchedulingType == SchedulingType.ScheduleParallelEnable)
        {
            var ECB = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            
            new DestroyStoppedEntitiesEnabled
            {
                ECB = ECB,
                elapsedTime = elapsedTime
            }.ScheduleParallel();
        }
    }
}

[BurstCompile]
public partial struct DestroyStoppedEntities : IJobEntity
{
    public float elapsedTime;
    public EntityCommandBuffer.ParallelWriter ECB; 
    public void Execute([ChunkIndexInQuery]int chunkKey,in StopRotatingTag stopRot, Entity entity)
    {
       if (elapsedTime >= stopRot.timer)
       {
            ECB.DestroyEntity(chunkKey, entity);
       }
    }
}
[BurstCompile]
[WithNone(typeof(RotatingData))]
public partial struct DestroyStoppedEntitiesEnabled : IJobEntity
{
    public float elapsedTime;
    public EntityCommandBuffer.ParallelWriter ECB; 
    public void Execute([ChunkIndexInQuery]int chunkKey, in RandomData randomData, Entity entity)
    {
        if (elapsedTime >= randomData.timer)
        {
            ECB.DestroyEntity(chunkKey, entity);
        }
    }
}

