using System;
using Unity.Entities;
using UnityEngine;


public class BakerExample : MonoBehaviour
{
    [Header("nonmutable variables")]
    public int Maxhealth;
    [Header("mutable variables")]
    [Tooltip("I am hero boys health")]
    public int health;
    [Range(0,30)]
    public float speed;
    public bool isOnFire;


    private void OnValidate()
    {
        if (health > Maxhealth)
        {
            health = Maxhealth;
        }
    }

    public class baker : Baker<BakerExample>
    {
        public override void Bake(BakerExample authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new HeroAttributes
            {
                maxHealth = authoring.Maxhealth,
                health = authoring.health,
                speed = authoring.speed,
                isOnFire = authoring.isOnFire
            });
        }
    }
        
}
public struct HeroAttributes : IComponentData
{
    public int maxHealth;
    public int health;
    public float speed;
    public bool isOnFire; 
}