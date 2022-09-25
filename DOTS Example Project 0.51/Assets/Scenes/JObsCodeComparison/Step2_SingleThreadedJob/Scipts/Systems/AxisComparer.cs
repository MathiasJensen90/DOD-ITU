using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
public struct AxisComparer : IComparer<Translation> 
{
    public int Compare(Translation a, Translation b)
    {
        return a.Value.x.CompareTo(b.Value.x);
    }
}
