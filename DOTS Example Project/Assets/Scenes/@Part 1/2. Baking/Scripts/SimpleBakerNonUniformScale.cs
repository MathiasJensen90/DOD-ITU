using Unity.Entities;
using UnityEngine;


    public class SimpleBakerNonUniformScale : MonoBehaviour
    {
        public float simpleValue; 
        public class baker : Baker<SimpleBakerNonUniformScale>
        {
            public override void Bake(SimpleBakerNonUniformScale authoring)
            {
                var entity = GetEntity(TransformUsageFlags.NonUniformScale);
                AddComponent(entity, new SimpleComp
                {
                    Value = authoring.simpleValue
                });
            }
        }
    }