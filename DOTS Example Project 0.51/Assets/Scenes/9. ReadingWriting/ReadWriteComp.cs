using Unity.Entities;
    [GenerateAuthoringComponent]
    public struct ReadWriteComp : IComponentData
    {
        public Entity entityRef;
        public float speed; 
    }
