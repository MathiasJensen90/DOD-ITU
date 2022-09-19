using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
    public struct EnemyComponent : IComponentData
    {
        //public float radius;
        public int health;
        public float takingDamageColdown;
    }
