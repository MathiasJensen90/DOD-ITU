using Unity.Entities;
using UnityEngine;

class NewBakerScript : MonoBehaviour
{
    public float speed = 1;
}

class NewBakerScriptBaker : Baker<NewBakerScript>
{
    public override void Bake(NewBakerScript authoring){
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new speedData{
            speed = authoring.speed
        });
    }
}

public struct speedData : IComponentData
{
    public float speed;
}