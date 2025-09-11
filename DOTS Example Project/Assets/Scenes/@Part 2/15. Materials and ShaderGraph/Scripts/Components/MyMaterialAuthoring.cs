using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;
class MyMaterialAuthoring : MonoBehaviour
{
    public Vector3 color; 
    class baker : Baker<MyMaterialAuthoring>
    {
        public override void Bake(MyMaterialAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Renderable);
            AddComponent(entity, new MyMaterial
            {
                Value = new float4(authoring.color, 1)
            });
        }
    }
}

[MaterialProperty("mainColor")]
public struct MyMaterial : IComponentData
{
    public float4 Value; 
}
