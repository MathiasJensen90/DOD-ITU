using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemyInteractionTagBaker : MonoBehaviour
{
    class baker : Baker<EnemyInteractionTagBaker>
    {
        public override void Bake(EnemyInteractionTagBaker authoring)
        {
            AddComponent<enemyInteractionTag>();
        }
    }
}


public struct enemyInteractionTag : IComponentData
{
    
}