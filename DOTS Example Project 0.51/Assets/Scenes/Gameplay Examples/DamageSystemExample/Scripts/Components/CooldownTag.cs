using Unity.Entities;
using UnityEngine;

public class CoolDownBaker : MonoBehaviour
{

    class baker : Baker<CoolDownBaker>
    {
        public override void Bake(CoolDownBaker authoring)
        {
            AddComponent<CoolDownTag>();
        }
    }
}
public struct CoolDownTag : IComponentData
{
    
}
