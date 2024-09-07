using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;
class MyMaterialAuthoring : MonoBehaviour
{
    class baker : Baker<MyMaterialAuthoring>
    {
        public override void Bake(MyMaterialAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Renderable);
            AddComponent<MyMaterial>(entity);
        }
    }
}

[MaterialProperty("mainColor")]
public struct MyMaterial : IComponentData
{
    public float4 Value; 
}


// [MaterialProperty("mainColor", MaterialPropertyFormat.Float4)]
// public struct MyMaterial : IComponentData
// {
//     public float4 Value; 
// }
