using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SimpleBakerDynamic : MonoBehaviour
{

    public float simpleValue; 
    
    public class baker : Baker<SimpleBakerDynamic>
    {
        public override void Bake(SimpleBakerDynamic authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SimpleComp
            {
                Value = authoring.simpleValue
            });
        }
    }
}

public struct SimpleComp : IComponentData
{
    public float Value; 
}