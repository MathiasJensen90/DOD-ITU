using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public partial class FaceTargetSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<GameplayInteractionSingleton>();
    }

    protected override void OnUpdate()
    {
        float dt = SystemAPI.Time.DeltaTime; 
       var translationArray = GetComponentLookup<LocalTransform>(true);

       var rotateTowardsPlayerJob = new RotateTowardsPlayerJob
       {
           dt = dt,
           transformArray = translationArray
       }.ScheduleParallel(Dependency);

       /*Entities.WithAll<ChaserTag>().ForEach((ref Rotation rotation, ref moveData moveData, in Translation trans, in TowerTarget target) =>
        {
            var targetPos = GetComponent<Translation>(target.Value).Value;
            var dir = targetPos - trans.Value;
            var normalisedDir = math.normalizesafe(dir);

            quaternion targetRot = quaternion.LookRotationSafe(normalisedDir, math.up());
            rotation.Value = math.slerp(rotation.Value, targetRot, dt * moveData.rotationSpeed);

        }).ScheduleParallel();
        */
       
       var rotateTowardsNearestTargetJob = new RotateTowardsNearestEnemyJob
       {
           dt = dt,
           localTransformArray = translationArray
       }.ScheduleParallel(rotateTowardsPlayerJob);
       /*Entities.WithAll<TowerTag>().ForEach((DynamicBuffer<EnemyTargetBuffer> enemyBuffer, ref Rotation rotation, ref moveData moveData, in Translation trans) =>
       {
           float closestDist = math.INFINITY;
           float3 targetPos = 0; 
           for (int i = 0; i < enemyBuffer.Length; i++)
           {
               var enemyTrans = GetComponent<Translation>(enemyBuffer[i].Value);
               var dist = math.distance(trans.Value, enemyTrans.Value);

               if (dist < closestDist)
               {
                   closestDist = dist;
                   targetPos = enemyTrans.Value;
               } 
           }

           var dir = targetPos - trans.Value;
           var normalisedDir = math.normalizesafe(dir);

           quaternion targetRot = quaternion.LookRotationSafe(normalisedDir, math.up());
           rotation.Value = math.slerp(rotation.Value, targetRot, dt * moveData.rotationSpeed);
       }).ScheduleParallel();
       */
       
       rotateTowardsNearestTargetJob.Complete();
    }
}



[WithAll(typeof(ChaserTag))]
[BurstCompile]
public partial struct RotateTowardsPlayerJob : IJobEntity
{
    public float dt;
    [ReadOnly]
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
    [ReadOnly]
    public ComponentLookup<LocalTransform> localTransformArray;
    public float dt;

    public void Execute(DynamicBuffer<EnemyTargetBuffer> enemyBuffer, ref LocalTransform localTrans, ref moveData moveData)
    {
        float closestDist = math.INFINITY;
        float3 targetPos = 0; 
        for (int i = 0; i < enemyBuffer.Length; i++)
        {
            var enemyPos = localTransformArray[enemyBuffer[i].Value].Position;
            var dist = math.distance(localTrans.Position, enemyPos);

            if (dist < closestDist)
            {
                closestDist = dist;
                targetPos = enemyPos;
            } 
        }
        var dir = targetPos - localTrans.Position;
        var normalisedDir = math.normalizesafe(dir);
        quaternion targetRot = quaternion.LookRotationSafe(normalisedDir, math.up());
        localTrans.Rotation = math.slerp(localTrans.Rotation, targetRot, dt * moveData.rotationSpeed);
    }
}