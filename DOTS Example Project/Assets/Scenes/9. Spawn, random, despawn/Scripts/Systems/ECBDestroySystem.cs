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
        var ECB = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
       
        if (ecbSingleton.SchedulingType == SchedulingType.Run)
        {
            foreach (var (stopRotTag,
                         entity)  in SystemAPI.Query<RefRO<StopRotatingTag>>().WithEntityAccess())
            {
                if (elapsedTime >= stopRotTag.ValueRO.timer)
                {
                    ECB.DestroyEntity(entity.Index, entity);
                }
            }
        }
        else if (ecbSingleton.SchedulingType == SchedulingType.Schedule)
        {
            
            new DestroyStoppedEntities
            {
                ECB = ECB,
                elapsedTime = elapsedTime
            }.Schedule();
        }
    }
}

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

