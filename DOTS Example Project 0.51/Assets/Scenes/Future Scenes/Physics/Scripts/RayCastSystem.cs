using System.Collections;
using System.Collections.Generic;
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
        PhysicsWorld physicWworld = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld;

        var physiscCol = GetComponentDataFromEntity<PhysicsCollider>();

        Entities.WithReadOnly(physicWworld).WithReadOnly(physiscCol).ForEach((in LocalToWorld ltw, in Raycaster raycaster) =>
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

            if (physicWworld.CastRay(input, out RaycastHit hit))
            {
                if (physiscCol.HasComponent(hit.Entity))
                {
                    Debug.DrawLine(input.Start, input.End, Color.red);
                }
            }
            else
            {
                Debug.DrawLine(input.Start, input.End, Color.blue);
            }

        }).ScheduleParallel();
        Dependency.Complete();
        
    }
}

