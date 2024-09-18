using Unity.Entities;using UnityEngine;

class TowerTagAuthoring : MonoBehaviour
{
    class baker : Baker<TowerTagAuthoring>
    {
        public override void Bake(TowerTagAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<TowerTag>(entity);
        }
    }
}

    public struct TowerTag : IComponentData
    {

    }
