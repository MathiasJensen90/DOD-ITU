using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerInteractionTagBaker : MonoBehaviour
{

    class baker : Baker<PlayerInteractionTagBaker>
    {
        public override void Bake(PlayerInteractionTagBaker authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerInteractionTag>(entity);
        }
    }
}


public struct PlayerInteractionTag : IComponentData
{
    
}