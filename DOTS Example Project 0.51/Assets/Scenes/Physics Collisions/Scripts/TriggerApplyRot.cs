using System.ComponentModel;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

public partial class TriggerApplyRot : SystemBase 
{
    private StepPhysicsWorld stepPhysicsWorld;

    protected override void OnCreate()
    {
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    protected override void OnUpdate()
    {
        var ecb = new EntityCommandBuffer(World.UpdateAllocator.ToAllocator);
        var ecb2 = new EntityCommandBuffer(World.UpdateAllocator.ToAllocator);
        var applyRotTag = new ApplyRotTag
        {
            allTriggerRotTags = GetComponentDataFromEntity<TriggerRotTag>(true),
            allAffectedTag = GetComponentDataFromEntity<AffectedTag>(true),
            ecb = ecb
        }.Schedule(stepPhysicsWorld.Simulation, Dependency);
        var ApplyNewScale = new ApplyNewScale
        {
            allApplyScaleTag = GetComponentDataFromEntity<ApplyScaleTag>(true),
            allAffectedTag = GetComponentDataFromEntity<AffectedTag>(true),
            ecb = ecb2
        }.Schedule(stepPhysicsWorld.Simulation, applyRotTag);
        
        applyRotTag.Complete();
        ApplyNewScale.Complete();
        ecb.Playback(EntityManager);
        ecb2.Playback(EntityManager);
    }
}


[BurstCompile]
public partial struct ApplyRotTag : ITriggerEventsJob
{
    [Unity.Collections.ReadOnly]
    public ComponentDataFromEntity<TriggerRotTag> allTriggerRotTags; 
    [Unity.Collections.ReadOnly]
    public ComponentDataFromEntity<AffectedTag> allAffectedTag;
    public EntityCommandBuffer ecb;

    public void Execute(TriggerEvent triggerEvent)
    {
        Entity entityA = triggerEvent.EntityA;
        Entity entityB = triggerEvent.EntityB;

        if (allTriggerRotTags.HasComponent(entityA) && allAffectedTag.HasComponent(entityB))
        {
            ecb.AddComponent<RotateTag>(entityB);
            ecb.AddComponent(entityB, new WaveDataComponent
            {
                rotationSpeed = 100
            });
        }
        else if (allTriggerRotTags.HasComponent(entityB) && allAffectedTag.HasComponent(entityA))
        {
            ecb.AddComponent<RotateTag>(entityA);
            ecb.AddComponent(entityA, new WaveDataComponent
            {
                rotationSpeed = 100
            });
        }
    }
}


[BurstCompile]
public partial struct ApplyNewScale : ITriggerEventsJob
{
    [Unity.Collections.ReadOnly]
    public ComponentDataFromEntity<ApplyScaleTag> allApplyScaleTag; 
    [Unity.Collections.ReadOnly]
    public ComponentDataFromEntity<AffectedTag> allAffectedTag;
    public EntityCommandBuffer ecb;

    public void Execute(TriggerEvent triggerEvent)
    {
        Entity entityA = triggerEvent.EntityA;
        Entity entityB = triggerEvent.EntityB;

        if (allApplyScaleTag.HasComponent(entityA) && allAffectedTag.HasComponent(entityB))
        {
            ecb.AddComponent(entityB, new NonUniformScale
            {
                Value = new float3(3, 3, 3)
            });
        }
        else if (allApplyScaleTag.HasComponent(entityB) && allAffectedTag.HasComponent(entityA))
        {
            ecb.AddComponent(entityA, new NonUniformScale
            {
                Value = new float3(3, 3, 3)
            });
        }
    }
}