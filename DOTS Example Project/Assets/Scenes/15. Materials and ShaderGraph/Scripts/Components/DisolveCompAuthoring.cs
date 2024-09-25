using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

class DisolveCompAuthoring : MonoBehaviour
{
    public float disolveValue; 
    class baker : Baker<DisolveCompAuthoring>
    {
        public override void Bake(DisolveCompAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Renderable);
            AddComponent(entity, new DisolveComp
            {
                Value = authoring.disolveValue
            });
        }
    }
}


[MaterialProperty("disolveRef")]
public struct DisolveComp : IComponentData
{
    public float Value;
}
