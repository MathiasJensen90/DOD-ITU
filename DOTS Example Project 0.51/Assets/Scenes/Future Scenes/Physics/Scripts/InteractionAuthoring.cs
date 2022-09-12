using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class InteractionAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    
    [Header("jumping pads")]
    public GameObject jumpingPads;
    [Header("InteractionObjects")]
    public GameObject interactionObject;
    [Header("Config variables")]
    public int interactionThreshold;
    public float3 obstacleDestination;
        
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddBuffer<ObstacleBuffer>(entity);
        dstManager.AddBuffer<JumpingpadsBuffer>(entity);
        
    
        dstManager.AddComponentData(entity, new InteractionConfig
        {
            interactionTreshold = interactionThreshold,
            obstacleDestination = (float3)interactionObject.transform.position + obstacleDestination
            
        });
        var interactionEntity = conversionSystem.GetPrimaryEntity(interactionObject);
        var jumpingEntity = conversionSystem.GetPrimaryEntity(jumpingPads);
        var interactionBuffer = dstManager.GetBuffer<ObstacleBuffer>(entity);
        var jumpingBuffer = dstManager.GetBuffer<JumpingpadsBuffer>(entity);
        
        interactionBuffer.Add(new ObstacleBuffer {Value = interactionEntity});
        jumpingBuffer.Add(new JumpingpadsBuffer {Value = jumpingEntity});
    }
}


public struct MoveObstacle : IComponentData
{
    public float3 destinationValue;
}

public struct ObstacleBuffer : IBufferElementData
{
    public Entity Value; 
}

public struct JumpingpadsBuffer : IBufferElementData
{
    public Entity Value; 
}

public struct InteractionConfig : IComponentData
{
    public int interactionTreshold;
    public float3 obstacleDestination;
}

