using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class EnemySystem : SystemBase
{
    private Unity.Mathematics.Random rng = new Unity.Mathematics.Random(1234);
    
    protected override void OnUpdate()
    {
        var raycaster = new MovementRaycast() { physicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld };
        rng.NextInt();
        var rngTemp = rng;

        Entities.ForEach((ref Enemy enemy, ref Movable movable, in Translation translation) =>
        {
            if (math.distance(translation.Value, enemy.previousCell) > 0.9f)
            {
                enemy.previousCell = math.round(translation.Value);

                // perform raycasts here
                var listOfValidDirections = new NativeList<float3>(Allocator.Temp);

                if (!raycaster.CheckRay(translation.Value, new float3(0, 0, 1), 
                    movable.direction)) listOfValidDirections.Add(new float3(0, 0, 1));
                
                if (!raycaster.CheckRay(translation.Value, new float3(0, 0, -1), 
                    movable.direction)) listOfValidDirections.Add(new float3(0, 0, -1));
                
                if (!raycaster.CheckRay(translation.Value, new float3(1, 0, 0), 
                    movable.direction)) listOfValidDirections.Add(new float3(1, 0, 0));
                
                if (!raycaster.CheckRay(translation.Value, new float3(-1, 0, 0), 
                    movable.direction)) listOfValidDirections.Add(new float3(-1, 0, 0));

                movable.direction = listOfValidDirections[rngTemp.NextInt(listOfValidDirections.Length)];

                listOfValidDirections.Dispose();
            }
        }).Schedule();
    }
    private struct MovementRaycast
    {
        [ReadOnly] public PhysicsWorld physicsWorld;

        public bool CheckRay(float3 position, float3 direction, float3 currentDirection)
        {

            if (direction.Equals(-currentDirection)) return true;
            
            var ray = new RaycastInput()
            {
                Start = position,
                End = position + (direction * 0.9f),
                Filter = new CollisionFilter()
                {
                    GroupIndex = 0,
                    BelongsTo = 1u << 1,
                    CollidesWith = 1u << 2
                }
            };
            return physicsWorld.CastRay(ray);
        }
        
    }
}
