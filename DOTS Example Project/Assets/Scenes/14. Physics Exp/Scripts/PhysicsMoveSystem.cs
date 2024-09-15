using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

partial struct PhysicsMoveSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<physicsPlayerComp>();
    }

    //[BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float dt = SystemAPI.Time.DeltaTime;
        UnityEngine.Plane plane = new UnityEngine.Plane(math.up(), float3.zero);
        var cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float3 hit;

        if (plane.Raycast(cameraRay, out float distance))
        {
            hit = cameraRay.GetPoint(distance);
        }
        else
        {
            hit = float3.zero;
        }

        foreach (var (trans,
                     physicsPlayerComp,
                     physicsVelocity) in SystemAPI.Query<RefRW<LocalTransform>,
                     RefRO<physicsPlayerComp>,
                     RefRW<PhysicsVelocity>>())
        {
            float3 targetPosition = trans.ValueRO.Position + hit;
            float3 moveDirection = targetPosition - trans.ValueRO.Position;
            moveDirection = math.normalize(moveDirection);

            float rotationsSpeed = 10f;

            trans.ValueRW.Rotation = math.slerp(trans.ValueRO.Rotation,
                quaternion.LookRotation(moveDirection, math.up()), dt * rotationsSpeed);
            
            physicsVelocity.ValueRW.Linear = moveDirection  * physicsPlayerComp.ValueRO.speed;
            physicsVelocity.ValueRW.Angular = float3.zero;

        }
    }
}
