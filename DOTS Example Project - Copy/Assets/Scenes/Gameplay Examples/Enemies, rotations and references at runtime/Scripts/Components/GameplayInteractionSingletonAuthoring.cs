using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

class GameplayInteractionSingletonAuthoring : MonoBehaviour
{
    class baker : Baker<GameplayInteractionSingletonAuthoring>
    {
        public override void Bake(GameplayInteractionSingletonAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<GameplayInteractionSingleton>(entity);
        }
    }
}

public struct GameplayInteractionSingleton : IComponentData
{

}
