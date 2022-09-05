using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
public class ColorAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public Color mainColor;
    public Color destinationColor;
    
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new MyMaterial
        {
            Value = new float4(mainColor.r, mainColor.g, mainColor.b, mainColor.a)
        });
        dstManager.AddComponentData(entity, new DestinationColor
        {
            Value = new float4(destinationColor.r, destinationColor.g, destinationColor.b, destinationColor.a)
        });
    }
}

public struct DestinationColor : IComponentData
{
    public float4 Value; 
}