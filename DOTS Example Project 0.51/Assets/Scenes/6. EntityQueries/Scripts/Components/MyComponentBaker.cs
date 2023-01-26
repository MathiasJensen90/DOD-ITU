using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class MyComponentBaker : MonoBehaviour
{

    class baker : Baker<MyComponentBaker>
    {
        public override void Bake(MyComponentBaker authoring)
        {
            AddComponent<MyComponent>();
            AddComponent<CooldownComponent>();
        }
    }
}
//You can declare more components in the same file, but it is best to avoid doing that for clarity
public struct MyComponent : IComponentData
{
    public float scale;
    public float3 position;
}

//This component should be put in its own script
public struct CooldownComponent : IComponentData
{
    public float Value; 
}