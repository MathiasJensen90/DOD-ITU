using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;
[RequireMatchingQueriesForUpdate]
[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(PhysicsSystemGroup))]
public partial struct CollisionSystem : ISystem
{
    private ComponentLookup<PhysicsPlayer> physicsPlayer;
    private ComponentLookup<PhysicsVelocity> physicsVelocity;
    private ComponentLookup<JumpingPlatform> jumpingPlatform;
    
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InteractionConfig>();
        physicsPlayer = state.GetComponentLookup<PhysicsPlayer>(true);
        jumpingPlatform = state.GetComponentLookup<JumpingPlatform>(true);
        physicsVelocity = state.GetComponentLookup<PhysicsVelocity>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        physicsPlayer.Update(ref state);
        physicsVelocity.Update(ref state);
        jumpingPlatform.Update(ref state);
        
        state.Dependency = new CollisionJob
        {
            allPlayer = physicsPlayer,
            allPhysicsVelocity = physicsVelocity,
            allJumpingPlatforms = jumpingPlatform
        }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
    }
}

[BurstCompile]
public struct CollisionJob  : ICollisionEventsJob
{
    [ReadOnly] public ComponentLookup<PhysicsPlayer> allPlayer;
    [ReadOnly] public ComponentLookup<JumpingPlatform> allJumpingPlatforms;
    public ComponentLookup<PhysicsVelocity> allPhysicsVelocity; 
    
    public void Execute(CollisionEvent collisionEvent)
    {
        Debug.Log("happening");
        Entity entityA = collisionEvent.EntityA;
        Entity entityB = collisionEvent.EntityB;

        if (allJumpingPlatforms.HasComponent(entityA) && allPlayer.HasComponent(entityB))
        {
            //Debug.Log("Happened");
            PhysicsVelocity physVelocity = allPhysicsVelocity[entityB];
            float velocityMod = allJumpingPlatforms[entityA].force;

            physVelocity.Linear += math.up() * velocityMod;
            allPhysicsVelocity[entityB] = physVelocity; 
        }
        else if (allJumpingPlatforms.HasComponent(entityB) && allPlayer.HasComponent(entityA))
        {
            //Debug.Log("Happened");
            PhysicsVelocity physVelocity = allPhysicsVelocity[entityA];
            float velocityMod = allJumpingPlatforms[entityB].force;

            physVelocity.Linear += math.up() * velocityMod;
            allPhysicsVelocity[entityA] = physVelocity; 
        }
    }
}



