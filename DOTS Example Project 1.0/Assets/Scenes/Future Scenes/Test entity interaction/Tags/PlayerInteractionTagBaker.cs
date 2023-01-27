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
            AddComponent<PlayerInteractionTag>();
        }
    }
}


public struct PlayerInteractionTag : IComponentData
{
    
}