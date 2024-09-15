// using Unity.Entities;
// using Unity.Mathematics;
// using Unity.Transforms;
// using UnityEngine;
//
//
//
// public partial class BulletMovementSystem : SystemBase 
// {
//     protected override void OnUpdate()
//     {
//         float dt = SystemAPI.Time.DeltaTime; 
//         var bulletMoveJob = new bulletMoveJob
//         {
//             dt = dt
//         }.ScheduleParallel(Dependency);
//         
//         bulletMoveJob.Complete();
//     }
// }
//
//
// public partial struct bulletMoveJob : IJobEntity
// {
//     public float dt;
//     public void Execute(ref LocalTransform trans, in LocalToWorld ltw, in Bullet bullet)
//     {
//         var forwardDir = ltw.Forward;
//         trans.Position += forwardDir * dt * bullet.speed;
//     }
// }
