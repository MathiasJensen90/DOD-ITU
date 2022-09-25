using System.ComponentModel;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class ApplyDamageSystem : SystemBase
{
    
    protected override void OnUpdate()
    { 
        
        var storage = GetStorageInfoFromEntity();
        var allTransforms = GetComponentDataFromEntity<Translation>(true);
        var allTookDamageArray = GetComponentDataFromEntity<TookDamage>(true);
        var ecb = new EntityCommandBuffer(World.UpdateAllocator.ToAllocator);
        

       var chedkDistJob = new CheckDistanceJob
        {
            translationArray = allTransforms,
            SIFE = storage
        }.Schedule();

        var damageJob = new DamageJob
        {
            tookDamageArray = allTookDamageArray,
            ecb = ecb,
            SIFE = storage
        }.Schedule(chedkDistJob);

        damageJob.Complete();
        
        ecb.Playback(EntityManager);
    }
}


[BurstCompile]
public partial struct CheckDistanceJob : IJobEntity
{
    [Unity.Collections.ReadOnly]
    public ComponentDataFromEntity<Translation> translationArray;
    public StorageInfoFromEntity SIFE; 
    public void Execute(ref DependencySingletonComponent singletonComp, in Translation trans)
    {
        if (!SIFE.Exists(singletonComp.targetEntity)) return;
            
         if (translationArray.TryGetComponent(singletonComp.targetEntity, out Translation enemyTrans))
             {
        float3 targetEntityPos = enemyTrans.Value;
        singletonComp.distance = math.distance(trans.Value, targetEntityPos);
                     
        //This is just for visualisation of range.
        Debug.DrawRay(trans.Value, math.normalizesafe(targetEntityPos) * singletonComp.radius,
            singletonComp.distance < singletonComp.radius ? Color.red : Color.magenta);
             }
    }
}

[BurstCompile]
public partial struct DamageJob : IJobEntity
{
    public StorageInfoFromEntity SIFE;
    [Unity.Collections.ReadOnly]
    public ComponentDataFromEntity<TookDamage> tookDamageArray;
    public EntityCommandBuffer ecb;  
    public void Execute(Entity entity, in DependencySingletonComponent singletonComp)
    {
        var enemyEntity = singletonComp.targetEntity;
        if (!SIFE.Exists(enemyEntity)) return;
            
        if (singletonComp.distance < singletonComp.radius)
        {
            if (!tookDamageArray.HasComponent(enemyEntity))
            {
                Debug.Log("took damage");
                ecb.AddComponent(enemyEntity, new TookDamage
                {
                    Value = singletonComp.damage
                });
            }
        }
    }
}
