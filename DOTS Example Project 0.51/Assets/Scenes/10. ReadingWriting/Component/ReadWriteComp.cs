using Unity.Entities;
  

    public struct ReadWriteComp : IComponentData
    {
        public Entity entityRef;
        public float speed; 
    }
