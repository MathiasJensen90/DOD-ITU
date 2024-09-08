using Unity.Burst;
using Unity.Entities;
using Unity.Rendering;
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
        var ECB = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
     .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
        
        new DestroyStoppedEntities
        {
            ECB = ECB,
            elapsedTime = elapsedTime
        }.Schedule();
    }
}

public partial struct DestroyStoppedEntities : IJobEntity
{
    public float elapsedTime;
    public EntityCommandBuffer.ParallelWriter ECB; 
    public void Execute([ChunkIndexInQuery]int chunkKey,in StopRotatingTag stopRot, Entity entity)
    {
       // if (stopRot.timer >= elapsedTime >= stopRot.timer)
       if (elapsedTime >= stopRot.timer)
        {
            ECB.DestroyEntity(chunkKey, entity);
        }
    }
}


// [UpdateAfter(typeof(ECBStopRotationSystem))]
//     public partial class ECBDestroySystem1 : SystemBase
//     {
//         private BeginSimulationEntityCommandBufferSystem ecbSystem;
//    
//         protected override void OnCreate()
//         {
//             RequireForUpdate<ECBSingletonComponent>();
//             ecbSystem = World.GetExistingSystem<BeginSimulationEntityCommandBufferSystem>();
//         }
//
//         protected override void OnUpdate()
//         {
//             var elapsedTime = Time.ElapsedTime;
//             EntityCommandBuffer.ParallelWriter ecb = ecbSystem.CreateCommandBuffer().AsParallelWriter();
//
//             Entities.ForEach((Entity entity, int entityInQueryIndex, in StopRotatingTag stopRot) =>
//             {
//                 if (stopRot.timer >= elapsedTime) return;
//                 ecb.DestroyEntity(entityInQueryIndex, entity);
//
//             }).Schedule();
//             
//             ecbSystem.AddJobHandleForProducer(Dependency);
//         }
//     }
