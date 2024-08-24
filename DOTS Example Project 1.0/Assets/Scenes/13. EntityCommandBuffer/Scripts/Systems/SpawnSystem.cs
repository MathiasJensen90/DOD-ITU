using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class SpawnSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<ECBSingletonComponent>();
    }

    protected  override void OnUpdate()
    {
        Enabled = false;
        var ecb = new EntityCommandBuffer(Allocator.TempJob);


        Entities.ForEach((ref LocalTransform trans) =>
        {

        }).Run();
        
       Entities.ForEach((Entity entity, in ECBSingletonComponent ecbSingleton) =>
        {
            
            for (int i = 0; i < ecbSingleton.spawnAmount; i++)
            {
                for (int j = 0; j < ecbSingleton.spawnAmount; j++)
                {
                    var e = ecb.Instantiate(ecbSingleton.prefabTospawn);
                    ecb.AddComponent(e, new LocalTransform {Position = new float3(i * 2, 0, j * 2)});
                }
            }
        }).Schedule();
        
        
        Dependency.Complete();
        ecb.Playback(EntityManager);
        ecb.Dispose();
    }
}
