using Unity.Entities;
using UnityEngine;

public class WaveDataBaker : MonoBehaviour
{
    public float amplitude;
    public float frequency;
    public float rotationSpeed;

    class baker : Baker<WaveDataBaker>
    {
        public override void Bake(WaveDataBaker authoring)
        {
            AddComponent(new SinWaveComponent
            {
                amplitude = authoring.amplitude,
                frequency = authoring.frequency,
                rotationSpeed = authoring.rotationSpeed
            });
        }
    }
}

public struct SinWaveComponent : IComponentData
{
    public float amplitude;
    public float frequency;
    public float rotationSpeed;
}
