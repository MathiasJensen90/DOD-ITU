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
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<enemyInteractionTag>(entity);
        }
    }
}


public struct enemyInteractionTag : IComponentData
{
    
}