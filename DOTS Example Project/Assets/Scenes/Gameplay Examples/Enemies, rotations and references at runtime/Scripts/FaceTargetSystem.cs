using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct FaceTargetSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<GameplayInteractionSingleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float dt = SystemAPI.Time.DeltaTime;
        var translationArray = state.GetComponentLookup<LocalTransform>(true);

        float3 towerPos = float3.zero;
        foreach (var trans in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<TowerTag>())
        {
            towerPos = trans.ValueRO.Position;
        }

        float closestDistance = float.MaxValue;
        float3 closestPoint = float3.zero;
        foreach (var trans in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<ChaserTag>())
        {
            float distance = math.distance(towerPos, trans.ValueRO.Position);
            if (distance < closestDistance)
            {
                closestPoint = trans.ValueRO.Position;
            }
        }

        new RotateTowardsPlayerJob
        {
            dt = dt,
            transformArray = translationArray
        }.ScheduleParallel();

        new RotateTowardsNearestEnemyJob
        {
            dt = dt,
            enemyPos = closestPoint
        }.ScheduleParallel();
    }

}


[WithAll(typeof(ChaserTag))]
[BurstCompile]
public partial struct RotateTowardsPlayerJob : IJobEntity
{
    public float dt;
    [ReadOnly]
    //[NativeDisableParallelForRestriction]
    [NativeDisableContainerSafetyRestriction]
    public ComponentLookup<LocalTransform> transformArray; 
    
    public void Execute(ref LocalTransform transform, ref moveData moveData, in TowerTarget target)
    {
        var targetPos = transformArray[target.Value].Position;
        var dir = targetPos - transform.Position;
        var normalisedDir = math.normalizesafe(dir);

        quaternion targetRot = quaternion.LookRotationSafe(normalisedDir, math.up());
        transform.Rotation = math.slerp(transform.Rotation , targetRot, dt * moveData.rotationSpeed);
    }
}

[WithAll(typeof(TowerTag))]
[BurstCompile]
public partial struct RotateTowardsNearestEnemyJob : IJobEntity
{
    public float dt;
    public float3 enemyPos;

    public void Execute(DynamicBuffer<EnemyTargetBuffer> enemyBuffer, ref LocalTransform localTrans, ref moveData moveData)
    {
        var dir = enemyPos - localTrans.Position;
        var normalisedDir = math.normalizesafe(dir);
        quaternion targetRot = quaternion.LookRotationSafe(normalisedDir, math.up());
        localTrans.Rotation = math.slerp(localTrans.Rotation, targetRot, dt * moveData.rotationSpeed);
    }
}