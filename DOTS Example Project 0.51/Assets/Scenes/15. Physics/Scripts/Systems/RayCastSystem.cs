using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;
using RaycastHit = Unity.Physics.RaycastHit;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial class RayCastSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var rayCastJob = new RayCastJob
        {
            physCol = GetComponentDataFromEntity<PhysicsCollider>(true),
            physWorld = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld
        }.Schedule();
        
        rayCastJob.Complete();
    }
}

public partial struct RayCastJob : IJobEntity
{
    [ReadOnly]
    public ComponentDataFromEntity<PhysicsCollider> physCol;
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
                CollidesWith = raycaster.collidesWith.Value,
                BelongsTo = raycaster.belongsTo.Value,
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

