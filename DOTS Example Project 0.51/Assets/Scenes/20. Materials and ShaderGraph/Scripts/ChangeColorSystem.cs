// using Unity.Burst;
// using Unity.Collections;
// using Unity.Entities;
// using Unity.Jobs;
// using Unity.Mathematics;
// using Unity.Transforms;
// using UnityEngine;
//
//
// public partial class ChangeColorSystem : SystemBase
// {
//     protected override void OnUpdate()
//     {
//         float dt = Time.DeltaTime;
//         float elapsedTime = (float)Time.ElapsedTime;
//
//         var colorLerpJob = new ColorLerpJob
//         {
//             dt = dt
//         }.Schedule();
//
//         var disolveJob = new disolveJob
//         {
//             elapsedTime = elapsedTime
//         }.Schedule();
//     }
// }
//
//
// [BurstCompile]
// public partial struct ColorLerpJob : IJobEntity
// {
//     public float dt; 
//     
//     public void Execute(ref MyMaterial color, in DestinationColor destColor, in PlayerInputComponent input)
//     {
//         if (input.input1Value)
//         {
//             color.Value = destColor.damageColor;
//         }
//         var colorLerp = math.lerp(color.Value, destColor.startingColor, dt);
//         color.Value = colorLerp; 
//     }
// }
//
// [BurstCompile]
// public partial struct disolveJob : IJobEntity
// {
//     public float elapsedTime; 
//     
//     public void Execute(ref DisolveComp disolveComp)
//     {
//         var disolveLerp = math.remap(-1, 1, 0, 1, math.sin(elapsedTime));
//         disolveComp.Value = disolveLerp;
//     }
// }
