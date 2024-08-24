using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PrefabConversion : MonoBehaviour
{
    public GameObject prefab; 
    class baker : Baker<PrefabConversion>
    {
        public override void Bake(PrefabConversion authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new prefabComp
            {
                entity = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}


public struct prefabComp : IComponentData
{
    public Entity entity; 
}