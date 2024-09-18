using Unity.Entities;
using UnityEngine;


public class SimbleBakerRenderable : MonoBehaviour
{

    public float simpleValue; 
    
    public class baker : Baker<SimbleBakerRenderable>
    {
        public override void Bake(SimbleBakerRenderable authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Renderable);
            AddComponent(entity, new SimpleComp
            {
                Value = authoring.simpleValue
            });
        }
    }
}