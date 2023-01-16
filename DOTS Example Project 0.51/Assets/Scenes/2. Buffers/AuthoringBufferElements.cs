using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
//
// [DisallowMultipleComponent]
// public class AuthoringBufferElements : MonoBehaviour, IConvertGameObjectToEntity
// {
//     public Transform[] enemyLocations;
//
//     public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
//     {
//         var buffer = dstManager.AddBuffer<EnemyLocationBuffer>(entity);
//         foreach (var enemeylocation in enemyLocations)
//         {
//             buffer.Add(new EnemyLocationBuffer {Value = enemeylocation.position});
//         }
//     }
// }
//
// public struct EnemyLocationBuffer : IBufferElementData
// {
//     public float3 Value; 
// }
//
