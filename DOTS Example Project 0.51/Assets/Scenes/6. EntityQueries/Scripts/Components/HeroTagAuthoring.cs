using Unity.Entities;
using UnityEngine;

public class HeroTagAuthoring : MonoBehaviour
{

    class baker : Baker<HeroTagAuthoring>
    {
        public override void Bake(HeroTagAuthoring authoring)
        {
            AddComponent<HeroTag>();
        }
    }
}
public struct HeroTag : IComponentData
{
    
}
