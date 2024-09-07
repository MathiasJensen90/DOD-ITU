using Unity.Entities;
using UnityEngine;

class ShaderSingleton : MonoBehaviour
{
    class ShaderSingletonBaker : Baker<ShaderSingleton>
    {
        public override void Bake(ShaderSingleton authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Renderable);
            AddComponent<ShaderSingletonComp>(entity);
        }
    }
}

public struct ShaderSingletonComp : IComponentData
{
    
}