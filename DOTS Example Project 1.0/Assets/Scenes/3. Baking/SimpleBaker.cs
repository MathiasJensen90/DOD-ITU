using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SimpleBaker : MonoBehaviour
{

    public float simpleValue; 
    
    public class baker : Baker<SimpleBaker>
    {
        public override void Bake(SimpleBaker authoring)
        {
            AddComponent(new SimpleComp
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