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
        RequireSingletonForUpdate<GameplayInteractionSingleton>();
    }

    protected override void OnUpdate()
    { 
        float dt = Time.DeltaTime;
       var translationArray = GetComponentDataFromEntity<Translation>(true);

       var rotateTowardsPlayerJob = new RotateTowardsPlayerJob
       {
           dt = dt,
           translationArray = translationArray
       }.ScheduleParallel();

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
           translationArray = translationArray
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
    public ComponentDataFromEntity<Translation> translationArray; 
    
    public void Execute(ref Rotation rotation, ref moveData moveData, in Translation trans, in TowerTarget target)
    {
        var targetPos = translationArray[target.Value].Value;
        var dir = targetPos - trans.Value;
        var normalisedDir = math.normalizesafe(dir);

        quaternion targetRot = quaternion.LookRotationSafe(normalisedDir, math.up());
        rotation.Value = math.slerp(rotation.Value, targetRot, dt * moveData.rotationSpeed);
    }
}

[WithAll(typeof(TowerTag))]
[BurstCompile]
public partial struct RotateTowardsNearestEnemyJob : IJobEntity
{
    [ReadOnly]
    public ComponentDataFromEntity<Translation> translationArray;
    public float dt;

    public void Execute(DynamicBuffer<EnemyTargetBuffer> enemyBuffer, ref Rotation rotation, ref moveData moveData, in Translation trans)
    {
        float closestDist = math.INFINITY;
        float3 targetPos = 0; 
        for (int i = 0; i < enemyBuffer.Length; i++)
        {
            var enemyPos = translationArray[enemyBuffer[i].Value].Value;
            var dist = math.distance(trans.Value, enemyPos);

            if (dist < closestDist)
            {
                closestDist = dist;
                targetPos = enemyPos;
            } 
        }
        var dir = targetPos - trans.Value;
        var normalisedDir = math.normalizesafe(dir);
        quaternion targetRot = quaternion.LookRotationSafe(normalisedDir, math.up());
        rotation.Value = math.slerp(rotation.Value, targetRot, dt * moveData.rotationSpeed);
    }
}