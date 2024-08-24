// using Unity.Burst;
// using Unity.Collections;
// using Unity.Entities;
// using Unity.Jobs;
// using Unity.Mathematics;
// using Unity.Transforms;
//
// [UpdateAfter(typeof(FaceTargetSystem))]
// public partial class EneemyMovementSystem : SystemBase
// {
//     protected override void OnUpdate()
//     {
//         float dt = SystemAPI.Time.DeltaTime; 
//
//         var enemyMovementJob = new EnemyMovementJob
//         {
//             dt = dt
//         }.Schedule(Dependency);
//         
//         enemyMovementJob.Complete();
//         
//         /*Entities.WithAll<ChaserTag>().ForEach((ref Translation translation, in Rotation rot, in moveData moveData) =>
//          {
//              float3 forwardDir = math.forward(rot.Value);
//              translation.Value +=  forwardDir * moveData.moveSpeed * dt;
//          }).ScheduleParallel();
//          */
//     }
// }
//
// [WithAll(typeof(ChaserTag))]
// [BurstCompile]
// public partial struct EnemyMovementJob : IJobEntity
// {
//     public float dt; 
//     public void Execute(ref Translation translation, in Rotation rot, in moveData moveData)
//     {
//         float3 forwardDir = math.forward(rot.Value);
//         translation.Value +=  forwardDir * moveData.moveSpeed * dt;
//     }
// }