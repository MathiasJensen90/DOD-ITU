using Unity.Entities;
using UnityEngine;


    class CooldownTagAuthoring : MonoBehaviour
    {
        class baker : Baker<CooldownTagAuthoring>
        {
            public override void Bake(CooldownTagAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Renderable);
                AddComponent<CooldownTag>(entity);
            }
        }
    }

    public struct CooldownTag : IComponentData
    {
        
    }


