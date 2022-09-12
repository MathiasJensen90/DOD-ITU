using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

public partial class CollisionSystem : SystemBase
{
    private StepPhysicsWorld stepPhysicsWorld;

    protected override void OnCreate()
    {
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    protected override void OnUpdate()
    {

        var collisionJob = new ColissionJob
        {
            allPlayer = GetComponentDataFromEntity<PhysicsPlayer>(),
            allPhysicsVelocity = GetComponentDataFromEntity<PhysicsVelocity>(),
            allJumpingPlatforms = GetComponentDataFromEntity<JumpingPlatform>()
        };

        Dependency = collisionJob.Schedule(stepPhysicsWorld.Simulation, Dependency);
        
        Dependency.Complete();
    }
}

[BurstCompile]
public struct ColissionJob : ICollisionEventsJob
{
    [ReadOnly] public ComponentDataFromEntity<PhysicsPlayer> allPlayer;
    [ReadOnly] public ComponentDataFromEntity<JumpingPlatform> allJumpingPlatforms;
    public ComponentDataFromEntity<PhysicsVelocity> allPhysicsVelocity; 
    
    public void Execute(CollisionEvent collisionEvent)
    {
        Entity entityA = collisionEvent.EntityA;
        Entity entityB = collisionEvent.EntityB;

        bool EntityAIsPlayer = allPlayer.HasComponent(entityA);
        bool EntityBIsPlatform = allJumpingPlatforms.HasComponent(entityB);

        if (EntityAIsPlayer && !EntityBIsPlatform || EntityBIsPlatform && !EntityAIsPlayer) return;

        if (EntityAIsPlayer && EntityBIsPlatform)
        {
            PhysicsVelocity physVelocity = allPhysicsVelocity[entityA];
            float velocityMod = allJumpingPlatforms[entityB].force;

            physVelocity.Linear += math.up() * velocityMod;

            allPhysicsVelocity[entityA] = physVelocity; 
        }
        if (EntityBIsPlatform && EntityAIsPlayer)
        {
            PhysicsVelocity physVelocity = allPhysicsVelocity[entityA];
            float velocityMod = allJumpingPlatforms[entityB].force;

            physVelocity.Linear += math.up() * velocityMod;

            allPhysicsVelocity[entityA] = physVelocity; 
        }
    }
}



