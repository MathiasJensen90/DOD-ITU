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
        var elapsedTime = Time.ElapsedTime;
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);

        Entities.WithAll<WaveDataComponent>().ForEach((Entity entity, int entityInQueryIndex, ref RandomData randData) =>
        {
            if (!randData.dataInitiliazed)
            {
   
                randData.Value.InitState((uint)entityInQueryIndex + 34);
                randData.dataInitiliazed = true;
            }
        }).Schedule();

        Entities.WithAll<WaveDataComponent>().WithNone<StopRotatingTag>().ForEach((Entity entity, int entityInQueryIndex, ref RandomData randData) =>
        {
            if (randData.timer > elapsedTime) return;
            if (randData.Value.NextInt(0,100) % 2 == 0)
            {
                ecb.AddComponent(entity, new StopRotatingTag
                {
                    timer = (float)elapsedTime + 2
                });
            }
            else
            {
                randData.timer += 3;
            }
        }).Schedule();
        
        Dependency.Complete();
        ecb.Playback(EntityManager);
        ecb.Dispose();
    }
}
