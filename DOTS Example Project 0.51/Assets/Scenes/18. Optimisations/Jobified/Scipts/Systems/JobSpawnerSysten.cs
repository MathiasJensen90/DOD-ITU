using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class JobSpawnerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Enabled = false;

     EntityCommandBuffer ecb = new EntityCommandBuffer(World.UpdateAllocator.ToAllocator);

        var rand = new Unity.Mathematics.Random(123);

        var targetAndSeekersInitJob = new TargetAndSeekersInitJob
        {
            ecb = ecb,
            randData = rand
        }.Schedule();
        
        targetAndSeekersInitJob.Complete();
        ecb.Playback(EntityManager);

    }
}

public partial struct TargetAndSeekersInitJob : IJobEntity
{
    public Unity.Mathematics.Random randData;
    public EntityCommandBuffer ecb; 
    
    public void Execute(ref InitData initData)
    {
        for (int i = 0; i < initData.NumSeekers; i++)
        {
            var entity = ecb.Instantiate(initData.SeekerPrefab);
            ecb.SetComponent(entity, new Translation
            {
                Value = new float3(randData.NextFloat(initData.bounds.x, 0),
                    0,
                    randData.NextFloat( initData.bounds.y, 0))
            });
            var rndomDir = new float3(randData.NextFloat(), 0, randData.NextFloat());
            ecb.AddComponent(entity, new SeekerData
            {
                direction = rndomDir
            });
        }

        for (int i = 0; i < initData.NumTargets; i++)
        {
            var entity = ecb.Instantiate(initData.TargetPrefab);
            ecb.SetComponent(entity, new Translation
            {
                Value = new float3(randData.NextFloat(initData.bounds.x, 0),
                    0,
                    randData.NextFloat( initData.bounds.y, 0))
            });
            var rndomDir = new float3(randData.NextFloat(), 0, randData.NextFloat());
            ecb.AddComponent(entity, new TargetData()
            {
                direction = rndomDir
            });
        }
    }
}