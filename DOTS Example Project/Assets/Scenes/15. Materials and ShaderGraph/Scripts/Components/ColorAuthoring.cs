using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
public class ColorAuthoring : MonoBehaviour
{
    public Color mainColor;
    public Color destinationColor;

    class baker : Baker<ColorAuthoring>
    {
        public override void Bake(ColorAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Renderable);
            AddComponent(entity, new MyMaterial
            {
                Value = new float4(authoring.mainColor.r,
                    authoring.mainColor.g,
                    authoring.mainColor.b,
                    authoring.mainColor.a)
            });
            AddComponent(entity, new DestinationColor
            {
                damageColor = new float4(authoring.destinationColor.r,
                    authoring.destinationColor.g,
                    authoring.destinationColor.b, 
                    authoring.destinationColor.a),
                startingColor = new float4(authoring.mainColor.r,
                    authoring.mainColor.g,
                    authoring.mainColor.b,
                    authoring.mainColor.a)
            });
        }
    }
}

public struct DestinationColor : IComponentData
{
    public float4 startingColor;
    public float4 damageColor;
    public float lerpvalue;
}


