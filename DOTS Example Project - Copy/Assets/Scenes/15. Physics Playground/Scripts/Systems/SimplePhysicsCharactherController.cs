using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct SimplePhysicsCharactherController : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InteractionConfig>();
    }
    public void OnUpdate(ref SystemState state)
    {
        float2 currentInput = new float2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float dt = SystemAPI.Time.DeltaTime;

        state.Dependency = new PhysicsPlayerMovementJob
        {
            dt = dt,
            currentInput = currentInput
        }.Schedule(state.Dependency);
    }
}

public partial struct PhysicsPlayerMovementJob : IJobEntity
{
    public float2 currentInput;
    public float dt; 
    public void Execute(ref PhysicsVelocity vel, in PhysicsPlayer player)
    {
        var newVelocity = vel.Linear.xz + currentInput * player.moveSpeed * dt;
        vel.Linear.xz = newVelocity;
    }
}