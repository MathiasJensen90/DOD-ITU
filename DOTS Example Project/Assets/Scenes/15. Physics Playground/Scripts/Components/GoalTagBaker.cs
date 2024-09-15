using Unity.Entities;
using UnityEngine;

class GoalTagBaker : MonoBehaviour
{
    class GoalTagBakerBaker : Baker<GoalTagBaker>
    {
        public override void Bake(GoalTagBaker authoring)
        {
            var e = GetEntity(TransformUsageFlags.Renderable);
            AddComponent<GoalTag>(e);
        }
    }
}

public struct GoalTag : IComponentData
{
    
}