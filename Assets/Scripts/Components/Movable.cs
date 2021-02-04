using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct Movable : IComponentData
{
    public float speed;
    public float3 direction;
}
