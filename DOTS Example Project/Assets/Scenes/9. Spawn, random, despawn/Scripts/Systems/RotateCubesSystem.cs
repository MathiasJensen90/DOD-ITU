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
        var ecbSingleton = SystemAPI.GetSingleton<ECBSingletonComponent>();

        if (ecbSingleton.SchedulingType == SchedulingType.Run)
        {
            foreach (var (trans,
                         rotData)  in SystemAPI.Query<RefRW<LocalTransform>,
                         RefRO<RotatingData>>())
            {
                var xRot = quaternion.RotateZ(rotData.ValueRO.Value * Mathf.Deg2Rad * dt);
                trans.ValueRW.Rotation = math.mul(trans.ValueRO.Rotation, xRot);
            }
            
        }
        else if (ecbSingleton.SchedulingType == SchedulingType.Schedule)
        {
            new RotateCubeJob
            {
                dt = dt
            }.ScheduleParallel();
        }
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
