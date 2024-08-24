// using System.Collections;
// using System.Collections.Generic;
// using Unity.Entities;
// using UnityEngine;
//
// public partial class AccessBlobSystem : SystemBase
// {
//     protected override void OnCreate()
//     {
//         RequireForUpdate<BlobSingleton>();
//     }
//
//     protected override void OnUpdate()
//     {
//         Enabled = false;
//         
//         Entities.ForEach((in WaypointComponent waypointComp) =>
//         {
//             Debug.Log("Place is: " + waypointComp.waypointBlobAssetRef.Value.waypointArray[0].waypontName);
//             Debug.Log("Place is: " + waypointComp.waypointBlobAssetRef.Value.waypointArray[1].waypontName);
//             Debug.Log("Place is: " + waypointComp.waypointBlobAssetRef.Value.waypointArray[2].waypontName);
//         }).WithoutBurst().Run();
//         
//     }
// }
//
//
