using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
//
// public class InteractionAuthoring : MonoBehaviour, IConvertGameObjectToEntity
// {
//     
//     [Header("jumping pads")]
//     public GameObject jumpingPads;
//     [Header("InteractionObjects")]
//     public GameObject interactionObject;
//     [Header("Config variables")]
//     public int interactionThreshold;
//     public float3 obstacleDestination;
//         
//     public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
//     {
//        dstManager.AddBuffer<ObstacleBuffer>(entity);
//
//        dstManager.AddComponentData(entity, new InteractionConfig
//         {
//             interactionTreshold = interactionThreshold,
//             obstacleDestination = (float3)interactionObject.transform.position + obstacleDestination
//             
//         });
//         var interactionEntity = conversionSystem.GetPrimaryEntity(interactionObject);
//         var obstacleBuffer = dstManager.GetBuffer<ObstacleBuffer>(entity);
//         obstacleBuffer.Add(new ObstacleBuffer {Value = interactionEntity});
//     }
// }
//
// public struct MoveObstacle : IComponentData
// {
//     public float3 destinationValue;
// }
//
// public struct ObstacleBuffer : IBufferElementData
// {
//     public Entity Value; 
// }
//
// public struct InteractionConfig : IComponentData
// {
//     public int interactionTreshold;
//     public float3 obstacleDestination;
// }

