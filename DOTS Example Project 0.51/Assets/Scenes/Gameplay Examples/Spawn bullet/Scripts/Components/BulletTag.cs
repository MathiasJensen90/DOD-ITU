using Unity.Entities;
using UnityEngine;


[GenerateAuthoringComponent]
public struct Bullet : IComponentData
{
    public float speed;
}
