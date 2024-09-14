using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;
using RaycastHit = Unity.Physics.RaycastHit;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct RayCastSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //var bla= SystemAPI.GetSingleton<BuildPhysicsWorld>();
        var physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>().PhysicsWorld;
        
        // var rayCastJob = new RayCastJob
        // {
        //     
        //     physCol = SystemAPI.GetComponentLookup<PhysicsCollider>(true),
        //     physWorld =  //SystemAPI.GetSingleton<BuildPhysicsWorld>() //World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld
        // }.Schedule();
        new RayCastJob
        {
            physCol = SystemAPI.GetComponentLookup<PhysicsCollider>(true),
            physWorld = physicsWorld
        }.Schedule();
        
        //rayCastJob.Complete();
    }
}

public partial struct RayCastJob : IJobEntity
{
    [ReadOnly]
    public ComponentLookup<PhysicsCollider> physCol;
    [ReadOnly]
    public PhysicsWorld physWorld;
    
    public void Execute(in LocalToWorld ltw, in Raycaster raycaster)
    {
        var dir = ltw.Forward;
        RaycastInput input = new RaycastInput
        {
            Start = ltw.Position,
            End = ltw.Position + raycaster.rayLengt * dir,
            Filter = new CollisionFilter
            {
                BelongsTo = ~0u,
                CollidesWith = (1 << 6) | (1 << 7),
                GroupIndex = 0
            }
        };

        if (physWorld.CastRay(input, out RaycastHit hit))
        {
            if (physCol.HasComponent(hit.Entity))
            {
                Debug.DrawLine(input.Start, input.End, Color.red);
            }
        }
        else
        {
            Debug.DrawLine(input.Start, input.End, Color.blue);
        }
    }
}

