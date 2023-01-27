using Unity.Entities;
using UnityEngine;

public class RotatingDataBaker : MonoBehaviour
{
    public float rotatingValue;
    
    class baker : Baker<RotatingDataBaker>
    {
        public override void Bake(RotatingDataBaker authoring)
        {
            AddComponent(new RotatingData
            {
                Value = authoring.rotatingValue
            });
        }
    }
}

public struct RotatingData : IComponentData
{
    public float Value; 
}