using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;


[GenerateAuthoringComponent]
[MaterialProperty("mainColor", MaterialPropertyFormat.Float4)]
public struct MyMaterial : IComponentData
{
    public float4 Value; 
}
