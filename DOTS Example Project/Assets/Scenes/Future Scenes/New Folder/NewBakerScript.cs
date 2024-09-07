using Unity.Entities;
using UnityEngine;

class NewBakerScript : MonoBehaviour
{
    public float myFloat = 4;
    class NewBakerScriptBaker : Baker<NewBakerScript>
    {
        public override void Bake(NewBakerScript authoring)
        {
           
        }
    }

}


