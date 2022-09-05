using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

    [GenerateAuthoringComponent]
    public struct CreateEntityComponent : IComponentData
    {
        public bool createEntityWithArchetype;
        public float3 position; 
    }
