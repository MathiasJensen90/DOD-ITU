using Unity.Entities;
using UnityEngine;


class EnemyComponentAuthoring : MonoBehaviour
{
    public int health;
    public float takingDamageColdown;
   
    
    class baker : Baker<EnemyComponentAuthoring>
    {
        public override void Bake(EnemyComponentAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Renderable);
            AddComponent(entity, new EnemyComponent
            {
                health = authoring.health,
                takingDamageColdown = authoring.takingDamageColdown
            });
        }
    }
}
    public struct EnemyComponent : IComponentData
    {
        //public float radius;
        public int health;
        public float takingDamageColdown;
    }
