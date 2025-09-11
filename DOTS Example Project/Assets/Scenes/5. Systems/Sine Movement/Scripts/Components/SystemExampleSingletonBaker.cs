using Unity.Entities;
using UnityEngine;

public class SystemExampleSingletonBaker : MonoBehaviour
{

    class baker : Baker<SystemExampleSingletonBaker>
    {
        public override void Bake(SystemExampleSingletonBaker authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<SystemExampleSingleton>(entity);
        }
    }
}

public struct SystemExampleSingleton : IComponentData
{

}