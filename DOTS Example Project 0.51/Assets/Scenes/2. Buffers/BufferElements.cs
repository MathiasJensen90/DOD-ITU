using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


[InternalBufferCapacity(50)]
public struct BufferElements : IBufferElementData
{
    public int Value; 
}


