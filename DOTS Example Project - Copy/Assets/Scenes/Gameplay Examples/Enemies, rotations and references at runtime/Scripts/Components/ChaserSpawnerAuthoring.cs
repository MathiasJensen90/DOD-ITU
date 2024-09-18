using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class ChaserSpawnerAuthoring : MonoBehaviour 
{
    public GameObject ChaserPrefab;
    public float timeDelay;
    class baker : Baker<ChaserSpawnerAuthoring>
    {
        public override void Bake(ChaserSpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ChaserSpawner
            {
                chaser = GetEntity(authoring.ChaserPrefab, TransformUsageFlags.Dynamic),
                timer = 0,
                timerDelay = authoring.timeDelay,
                random = new Random(1231)
            });
        }
    }
}


public struct ChaserSpawner : IComponentData
    {
        public Entity chaser;
        public float timer;
        public float timerDelay;
        public Random random;
    }
