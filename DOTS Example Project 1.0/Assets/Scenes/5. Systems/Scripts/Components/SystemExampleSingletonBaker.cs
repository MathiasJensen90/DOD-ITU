using Unity.Entities;
using UnityEngine;

public class SystemExampleSingletonBaker : MonoBehaviour
{

    class baker : Baker<SystemExampleSingletonBaker>
    {
        public override void Bake(SystemExampleSingletonBaker authoring)
        {
            AddComponent<SystemExampleSingleton>();
        }
    }
}

public struct SystemExampleSingleton : IComponentData
{

}