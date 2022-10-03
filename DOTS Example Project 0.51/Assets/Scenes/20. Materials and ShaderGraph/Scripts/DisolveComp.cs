using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

[GenerateAuthoringComponent]
[MaterialProperty("disolveRef", MaterialPropertyFormat.Float)]
public struct DisolveComp : IComponentData
{
    public float Value;
}
