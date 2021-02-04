using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

public class MovableSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref PhysicsVelocity physicsVelocity, in Movable mov) =>
        {
            var step = mov.direction * mov.speed;
            physicsVelocity.Linear = step;
        }).Schedule();
    }
}
