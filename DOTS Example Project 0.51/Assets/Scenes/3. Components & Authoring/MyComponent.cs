using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;



//you can only have one [GenerateAuthoringComponent] per script.
//You can declare more components in the same file, but it is best to avoid doing that for clarity
[GenerateAuthoringComponent]
public struct MyComponent : IComponentData
{
    public float scale;
    public float3 position;
}

//This component should be put in its own script
public struct CooldownComponent : IComponentData
{
    public float Value; 
}
