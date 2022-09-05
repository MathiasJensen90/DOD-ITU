using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


[GenerateAuthoringComponent]
public struct BufferElements : IBufferElementData
{
    public int Value; 
}