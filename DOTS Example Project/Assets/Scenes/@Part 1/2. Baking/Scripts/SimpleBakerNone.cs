using Unity.Entities;
using UnityEngine;

public class SimpleBakerNone : MonoBehaviour
{
    public float simpleValue; 
    public class baker : Baker<SimpleBakerNone>
    {
        public override void Bake(SimpleBakerNone authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new SimpleComp
            {
                Value = authoring.simpleValue
            });
        }
    }
}