using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct EnemySpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<GameplayInteractionSingleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float elapsedTime = (float)SystemAPI.Time.ElapsedTime;

         EntityCommandBuffer ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
         state.Dependency = new EnemeySpawnJob
         {
             elapsedTime = elapsedTime,
             ecb = ecb
         }.Schedule(state.Dependency);
         state.Dependency.Complete();
        ecb.Playback(state.EntityManager);

        // EntityCommandBuffer ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
        //     .CreateCommandBuffer(state.WorldUnmanaged);
        // new EnemeySpawnJob
        // {
        //     elapsedTime = elapsedTime,
        //     ecb = ecb
        // }.Schedule();
        
        
        
        //state.Dependency.Complete();
        
        // var ECB = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
        //     .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
        // new EnemeySpawnJobParallel
        // {
        //     elapsedTime = elapsedTime,
        //     ecb = ECB
        // }.ScheduleParallel();
        
    }
}


[BurstCompile]
public partial struct EnemeySpawnJob : IJobEntity
{
    public float elapsedTime;
    public EntityCommandBuffer ecb; 
    public void Execute(ref ChaserSpawner chaserSpawn, in LocalTransform trans)
    {
        if (elapsedTime >= chaserSpawn.timer)
        {
            chaserSpawn.timer = elapsedTime + chaserSpawn.timerDelay;
            Entity e = ecb.Instantiate(chaserSpawn.chaser); 
            
           ecb.SetComponent(e, new LocalTransform
           {
               Position = trans.Position + new float3(
                   chaserSpawn.random.NextFloat(-11, 11),
                   0,
                   chaserSpawn.random.NextFloat(-8, 8)), 
               Rotation = quaternion.identity,
               Scale = 1f
           });
            ecb.SetComponent(e, new moveData
            {
                moveSpeed = chaserSpawn.random.NextFloat(2, 6),
                rotationSpeed = chaserSpawn.random.NextFloat(.3f, .7f)
            });
        }
    }
}



[BurstCompile]
public partial struct EnemeySpawnJobParallel : IJobEntity
{
    public float elapsedTime;
    public EntityCommandBuffer.ParallelWriter ecb; 
    public void Execute([ChunkIndexInQuery] int chunkIndex, ref ChaserSpawner chaserSpawn, in LocalTransform trans)
    {
        if (elapsedTime >= chaserSpawn.timer)
        {
            chaserSpawn.timer = elapsedTime + chaserSpawn.timerDelay;
            Entity e = ecb.Instantiate(chunkIndex, chaserSpawn.chaser); 
            
            ecb.SetComponent(chunkIndex, e, new LocalTransform
            {
                Position = trans.Position + new float3(
                    chaserSpawn.random.NextFloat(-11, 11),
                    0,
                    chaserSpawn.random.NextFloat(-8, 8)), 
                Rotation = quaternion.identity,
                Scale = 1f
            });
            ecb.SetComponent(chunkIndex, e, new moveData
            {
                moveSpeed = chaserSpawn.random.NextFloat(2, 6),
                rotationSpeed = chaserSpawn.random.NextFloat(.3f, .7f)
            });
        }
    }
}
