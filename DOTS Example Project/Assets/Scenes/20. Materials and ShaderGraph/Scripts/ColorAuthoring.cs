// using Unity.Entities;
// using Unity.Mathematics;
// using UnityEngine;
//
// [DisallowMultipleComponent]
// public class ColorAuthoring : MonoBehaviour, IConvertGameObjectToEntity
// {
//     public Color mainColor;
//     public Color destinationColor;
//     
//     public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
//     {
//         dstManager.AddComponentData(entity, new MyMaterial
//         {
//             Value = new float4(mainColor.r, mainColor.g, mainColor.b, mainColor.a)
//         });
//         dstManager.AddComponentData(entity, new DestinationColor
//         {
//             damageColor = new float4(destinationColor.r, destinationColor.g, destinationColor.b, destinationColor.a),
//             startingColor = new float4(mainColor.r, mainColor.g, mainColor.b, mainColor.a)
//         });
//     }
// }
//
// public struct DestinationColor : IComponentData
// {
//     public float4 startingColor;
//     public float4 damageColor;
// }