using Unity.Entities;
using UnityEngine;


public class RoateTagBaker : MonoBehaviour 
{
       class baker : Baker<RoateTagBaker>
       {
              public override void Bake(RoateTagBaker authoring)
              {
                     AddComponent<RotateTag>();
              }
       }
}

public struct RotateTag : IComponentData
{

}
