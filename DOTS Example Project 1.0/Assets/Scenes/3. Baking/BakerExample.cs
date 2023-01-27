using Unity.Entities;
using UnityEngine;


public class BakerExample : MonoBehaviour
{
    public int health;
    public float speed;
    public bool isOnFire; 

    public class baker : Baker<BakerExample>
    {
        public override void Bake(BakerExample authoring)
        {
            AddComponent(new HeroAttributes
            {
                health = authoring.health,
                speed = authoring.speed,
                isOnFire = authoring.isOnFire
            });
        }
    }
        
}
public struct HeroAttributes : IComponentData
{
    public int health;
    public float speed;
    public bool isOnFire; 
}