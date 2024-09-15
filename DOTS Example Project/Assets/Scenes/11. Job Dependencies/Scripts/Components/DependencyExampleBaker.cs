using Unity.Entities;
using UnityEngine;

class DependencyExampleBaker : MonoBehaviour
{
    class DependencyExampleBakerBaker : Baker<DependencyExampleBaker>
    {
        public override void Bake(DependencyExampleBaker authoring)
        {
            var e = GetEntity(TransformUsageFlags.None);
            AddComponent<JobDepedencySingleton>(e);
        }
    }
}

public struct JobDepedencySingleton : IComponentData
{
    
}