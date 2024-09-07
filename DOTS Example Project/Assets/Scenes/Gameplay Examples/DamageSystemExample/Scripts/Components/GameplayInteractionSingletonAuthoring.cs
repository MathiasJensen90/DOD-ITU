using Unity.Entities;
using UnityEngine;


// class TookDamageAuthoring : MonoBehaviour
// {
//     public int health;
//     public float takingDamageColdown;
//    
//     
//     class baker : Baker<TookDamageAuthoring>
//     {
//         public override void Bake(TookDamageAuthoring authoring)
//         {
//             var entity = GetEntity(TransformUsageFlags.Renderable);
//             AddComponent(entity, new EnemyComponent
//             {
//                 
//             });
//         }
//     }
// }
    public struct TookDamage : IComponentData
    {
        public int Value;
    }
