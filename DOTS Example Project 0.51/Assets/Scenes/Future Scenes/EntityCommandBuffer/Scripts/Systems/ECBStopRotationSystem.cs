using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

[UpdateAfter(typeof(SpawnSystem))]
public partial class ECBStopRotationSystem : SystemBase
{

    protected override void OnCreate()
    {
        RequireSingletonForUpdate<ECBSingletonComponent>();
    }

    protected override void OnUpdate()
    {
        var elapsedTime = (float)Time.ElapsedTime;
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);

        var job = new InitRandomJob
        {
        }.Schedule();
        
        var job2 =  new StopRotatingJob
        {
            elapsedTime = elapsedTime,
            ecb = ecb
        }.Schedule(job);
        
        
       job2.Complete();
       
        ecb.Playback(EntityManager);
        ecb.Dispose();
    }
}


[WithAll(typeof(WaveDataComponent))]
public partial struct InitRandomJob : IJobEntity
{
    public void Execute([EntityInQueryIndex] int entityIndex,  ref RandomData randData)
    {
        if (!randData.dataInitiliazed)
        {
            randData.Value.InitState( (uint)entityIndex + 34);
            randData.dataInitiliazed = true;
        }
    }
}

[WithAll(typeof(WaveDataComponent))]
[WithNone(typeof(StopRotatingTag))]
public partial struct StopRotatingJob : IJobEntity
{
    public float elapsedTime;
    public EntityCommandBuffer ecb;
    public void Execute(Entity entity, ref RandomData randData)
    {
        if (randData.timer > elapsedTime) return;
        if (randData.Value.NextInt(0,100) % 2 == 0)
        {
            ecb.AddComponent(entity, new StopRotatingTag
            {
                timer = elapsedTime + 2
            });
        }
        else
        {
            randData.timer += 3;
        }
    }
}

