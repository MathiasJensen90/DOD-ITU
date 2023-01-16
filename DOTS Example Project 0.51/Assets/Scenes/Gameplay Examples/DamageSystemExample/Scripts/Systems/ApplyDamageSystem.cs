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
        
        var storage = GetEntityStorageInfoLookup();
        var allTransforms = GetComponentLookup<Translation>(true);
        var allTookDamageArray = GetComponentLookup<TookDamage>(true);
        var ecb = new EntityCommandBuffer(World.UpdateAllocator.ToAllocator);
        

       var chedkDistJob = new CheckDistanceJob
        {
            translationArray = allTransforms,
            SIFE = storage
        }.Schedule(Dependency);

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
    public ComponentLookup<Translation> translationArray;
    public EntityStorageInfoLookup SIFE; 
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
    public EntityStorageInfoLookup SIFE;
    [Unity.Collections.ReadOnly]
    public ComponentLookup<TookDamage> tookDamageArray;
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
