using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PrefabBaker : MonoBehaviour
{
    public GameObject prefab; 
    class baker : Baker<PrefabBaker>
    {
        public override void Bake(PrefabBaker authoring)
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