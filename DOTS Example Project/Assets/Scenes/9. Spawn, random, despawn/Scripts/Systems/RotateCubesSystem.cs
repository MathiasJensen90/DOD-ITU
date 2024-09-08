using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

partial struct RotateCubesSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ECBSingletonComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float dt = SystemAPI.Time.DeltaTime;

        new RotateCubeJob
        {
            dt = dt
        }.ScheduleParallel();
    }
}

[BurstCompile]
[WithNone(typeof(StopRotatingTag))]
public partial struct RotateCubeJob : IJobEntity
{
    public float dt;
    public void Execute( 
        ref LocalTransform trans, 
        in RotatingData rotData,
        in RandomData randomData)
    {
        var xRot = quaternion.RotateZ(rotData.Value * Mathf.Deg2Rad * dt);
        trans.Rotation = math.mul(trans.Rotation, xRot);
    }
}
