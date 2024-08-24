using Unity.Entities;
using UnityEngine;

[RequireMatchingQueriesForUpdate]
[UpdateAfter(typeof(SystemOrdering3))]
public partial struct SystemOrdering2 : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
       
        }

        public void OnDestroy(ref SystemState state)
        {
           
        }

        public void OnUpdate(ref SystemState state)
        {
            Debug.Log("Hello, I'm SystemOrdering2");
            state.Enabled = false;
        }
    }
