using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;


public struct ChaserSpawner : IComponentData
    {
        public Entity chaser;
        public float timer;
        public float timerDelay;
        public Random random;
    }
