using Unity.Entities;
using UnityEngine;


public class RotateTagBaker : MonoBehaviour 
{
       class baker : Baker<RotateTagBaker>
       {
              public override void Bake(RotateTagBaker authoring)
              {
                     AddComponent<RotateTag>();
              }
       }
}

public struct RotateTag : IComponentData
{

}