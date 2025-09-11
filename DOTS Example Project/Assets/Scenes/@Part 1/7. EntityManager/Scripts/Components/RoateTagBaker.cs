using Unity.Entities;
using UnityEngine;


public class RoateTagBaker : MonoBehaviour 
{
       class baker : Baker<RoateTagBaker>
       {
              public override void Bake(RoateTagBaker authoring)
              {
                     var entity = GetEntity(TransformUsageFlags.Dynamic);
                     AddComponent<RotateTag>(entity);
              }
       }
}

public struct RotateTag : IComponentData
{

}
