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
            AddComponent(new prefabComp
            {
                entity = GetEntity(authoring.prefab)
            });
        }
    }
}


public struct prefabComp : IComponentData
{
    public Entity entity; 
}