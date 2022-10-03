using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Config : MonoBehaviour
{
    public GameObject Prefab;
    public uint Size;
    public float Radius;

    public class MyBaker : Baker<Config>
    {
        public override void Bake(Config authoring)
        {
            AddComponent(new StateComp
            {
                prefab = GetEntity(authoring.Prefab),
                size = authoring.Size,
                radius = authoring.Radius
            });
        }
    }
}

public struct StateComp : IComponentData
{
    public uint size;
    public float radius;
    public Entity prefab; 
}