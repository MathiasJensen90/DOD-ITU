using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial class SimplePhysicsCharactherController : SystemBase
{
    protected override void OnUpdate()
    {
        float2 currentInput = new float2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float dt = Time.DeltaTime;
        
        Entities.ForEach((ref Translation translation, ref PhysicsVelocity vel,  in Rotation rotation, in PhysicsPlayer player) =>
        {
            var newVelocity = vel.Linear.xz + currentInput * player.moveSpeed * dt;
            vel.Linear.xz = newVelocity;

        }).Schedule();
    }
}
