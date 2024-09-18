using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class CubeSpawnerAuthoring : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate;

    class CubeSpawnerBaker: Baker<CubeSpawnerAuthoring>
    {
        public override void Bake(CubeSpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            float3 randomSpawnPos = new float3(
                 UnityEngine.Random.Range(-10f, 10f), // Random X position between -10 and 10
                 UnityEngine.Random.Range(0f, 5f),    // Random Y position between 0 and 5
                 UnityEngine.Random.Range(-10f, 10f)  // Random Z position between -10 and 10
             );

            AddComponent(entity, new CubeSpawnerComponent
            {
                prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                spawnPos = randomSpawnPos,
                nextSpawnTime = 0.0f,
                spawnRate = authoring.spawnRate,
            });
        }
    }
}
