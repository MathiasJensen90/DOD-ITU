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
       //var allTranslations = GetComponentDataFromEntity<Translation>(true);

       Entities.WithAll<ChaserTag>().ForEach((ref Rotation rotation, ref moveData moveData, in Translation trans, in TowerTarget target) =>
        {
            var targetPos = GetComponent<Translation>(target.Value).Value;
            //var targetPos = allTranslations[target.Value].Value;

            var dir = targetPos - trans.Value;
            var normalisedDir = math.normalizesafe(dir);

            quaternion targetRot = quaternion.LookRotationSafe(normalisedDir, math.up());
            rotation.Value = math.slerp(rotation.Value, targetRot, dt * moveData.rotationSpeed);

        }).ScheduleParallel();
       
       
       float closestDist = math.INFINITY;
       float3 targetPos = 0; 
       Entities.WithAll<TowerTag>().ForEach((DynamicBuffer<EnemyTargetBuffer> enemyBuffer, ref Rotation rotation, ref moveData moveData, in Translation trans) =>
       {
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
    }
}
