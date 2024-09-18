using Unity.Entities;
using UnityEngine;



public class ChaserTagAuthoring : MonoBehaviour 
{
    class baker : Baker<ChaserTagAuthoring>
    {
        public override void Bake(ChaserTagAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<ChaserTag>(entity);
        }
    }
}

    public struct ChaserTag : IComponentData
    {
        
    }
