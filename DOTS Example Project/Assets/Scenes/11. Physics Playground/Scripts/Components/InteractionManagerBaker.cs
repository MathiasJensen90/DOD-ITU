using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

class InteractionManagerBaker : MonoBehaviour
{
    // [Header("jumping pads")]
    //  public GameObject jumpingPads;
      [Header("InteractionObjects")]
     public GameObject interactionObject;
     public float3 destinationOffset;
     [Header("Config variables")]
     public int interactionThreshold;
   
    class InteractionManagerBakerBaker : Baker<InteractionManagerBaker>
    {
        public override void Bake(InteractionManagerBaker authoring)
        {
            var e = GetEntity(TransformUsageFlags.None);
            // AddBuffer<ObstacleBuffer>(e);
            AddComponent(e, new InteractionConfig
            {
                interactionTreshold = authoring.interactionThreshold,
                destinationOffset = authoring.destinationOffset,
                obstacle = GetEntity(authoring.interactionObject,TransformUsageFlags.Dynamic)
            });
        }
    }
}


public struct InteractionConfig : IComponentData
{
    public int interactionTreshold;
    public float3 destinationOffset;
    public Entity obstacle;
}

public struct MoveObstacle : IComponentData
{
    public float3 destinationValue;
    public bool initLerp;
}

// public struct ObstacleBuffer : IBufferElementData
// {
//     public Entity Value; 
// }

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

