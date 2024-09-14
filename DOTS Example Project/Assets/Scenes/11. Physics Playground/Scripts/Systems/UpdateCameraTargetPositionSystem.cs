using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct UpdateCameraTargetPositionSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InteractionConfig>();
    }
    public void OnUpdate(ref SystemState state)
    {
        foreach (var trans in SystemAPI.Query< RefRO<LocalTransform>>().WithAll<PhysicsPlayer>())
        {
            SimpleCameraFollow.Instance.UpdateTargetPosition(trans.ValueRO.Position);
        }
    }
}

