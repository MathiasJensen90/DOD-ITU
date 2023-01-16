// using System.Collections;
// using System.Collections.Generic;
// using Unity.Entities;
// using Unity.Mathematics;
// using Unity.Transforms;
// using UnityEngine;
//
// public partial class WaveMoveSystem : SystemBase
// {
//     protected override void OnCreate()
//     {
//        RequireForUpdate<SystemExampleSingleton>();
//     }
//
//     protected override void OnUpdate()
//     {
//         var elapsedTime = (float)Time.ElapsedTime;
//
//         var sinMovementJob = new SinMovementJob
//         {
//             elapsedTime = elapsedTime
//         };
//         sinMovementJob.ScheduleParallel();
//         
//         
//     }
// }
//
// public partial struct SinMovementJob : IJobEntity
// {
//     public float elapsedTime;
//     public void Execute(ref Translation trans, in WaveDataComponent waveData)
//     {
//         var waveMovement = waveData.amplitude * math.sin(elapsedTime * waveData.frequency) + trans.Value.y;
//         trans.Value = new float3(trans.Value.x, waveMovement, trans.Value.z);
//     }
// }
//
