using Unity.Entities;
using UnityEngine;


    public partial struct SystemOrdering3 : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            
        }

        public void OnDestroy(ref SystemState state)
        {
          
        }

        public void OnUpdate(ref SystemState state)
        {
            Debug.Log("Hello, I'm SystemOrdering3");
            state.Enabled = false;
        }
    }
