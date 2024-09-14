using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

public partial struct CollisionSystem : ISystem
{
    //private StepPhysicsWorld stepPhysicsWorld;
    private ComponentLookup<PhysicsPlayer> physicsPlayer;
    private ComponentLookup<PhysicsVelocity> physicsVelocity;
    private ComponentLookup<JumpingPlatform> jumpingPlatform;
    
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InteractionConfig>();
        physicsPlayer = new ComponentLookup<PhysicsPlayer>();
        physicsVelocity = new ComponentLookup<PhysicsVelocity>();
        jumpingPlatform = new ComponentLookup<JumpingPlatform>();
        //stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        physicsPlayer.Update(ref state);
        physicsVelocity.Update(ref state);
        jumpingPlatform.Update(ref state);
        
        state.Dependency = new ColissionJob
        {
            allPlayer = physicsPlayer,
            allPhysicsVelocity = physicsVelocity,
            allJumpingPlatforms = jumpingPlatform
        }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        
        //collisionJob.Complete();
    }
}

[BurstCompile]
public struct ColissionJob : ICollisionEventsJob
{
    [ReadOnly] public ComponentLookup<PhysicsPlayer> allPlayer;
    [ReadOnly] public ComponentLookup<JumpingPlatform> allJumpingPlatforms;
    public ComponentLookup<PhysicsVelocity> allPhysicsVelocity; 
    
    public void Execute(CollisionEvent collisionEvent)
    {
        Entity entityA = collisionEvent.EntityA;
        Entity entityB = collisionEvent.EntityB;

        if (allJumpingPlatforms.HasComponent(entityA) && allPlayer.HasComponent(entityB))
        {
            PhysicsVelocity physVelocity = allPhysicsVelocity[entityA];
            float velocityMod = allJumpingPlatforms[entityB].force;

            physVelocity.Linear += math.up() * velocityMod;

            allPhysicsVelocity[entityA] = physVelocity; 
        }
        else if (allJumpingPlatforms.HasComponent(entityB) && allPlayer.HasComponent(entityA))
        {
            PhysicsVelocity physVelocity = allPhysicsVelocity[entityA];
            float velocityMod = allJumpingPlatforms[entityB].force;

            physVelocity.Linear += math.up() * velocityMod;

            allPhysicsVelocity[entityA] = physVelocity; 
        }
    }
}



