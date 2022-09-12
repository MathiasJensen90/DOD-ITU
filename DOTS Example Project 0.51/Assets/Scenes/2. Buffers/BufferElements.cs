using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


[GenerateAuthoringComponent]
[InternalBufferCapacity(50)]
public struct BufferElements : IBufferElementData
{
    public int Value; 
}


