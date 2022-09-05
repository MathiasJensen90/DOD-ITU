using Unity.Entities;
using UnityEngine;

    public struct EnemyTargetBuffer : IBufferElementData
    {
        public Entity Value; 
    }

    public struct TowerTarget : IComponentData
    {
        public Entity Value; 
    }




