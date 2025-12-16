using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

[BurstCompile]
[UpdateInGroup(typeof(PhysicsSimulationGroup))]
public partial struct BallBrickCollisionSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ScoreData>();
    }

    public void OnUpdate(ref SystemState state)
    {
        var score = SystemAPI.GetSingletonRW<ScoreData>();

        var brickLookup = SystemAPI.GetComponentLookup<BrickData>();
        var ballLookup = SystemAPI.GetComponentLookup<BallTag>();

        var ecb = new EntityCommandBuffer(Allocator.Temp);

        state.Dependency = new CollisionJob
        {
            BrickLookup = brickLookup,
            BallLookup = ballLookup,
            Score = score,
            ECB = ecb
        }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);

        state.Dependency.Complete();

        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }

    [BurstCompile]
    private struct CollisionJob : ICollisionEventsJob
    {
        public ComponentLookup<BrickData> BrickLookup;
        [ReadOnly] public ComponentLookup<BallTag> BallLookup;

        public RefRW<ScoreData> Score;
        public EntityCommandBuffer ECB;

        public void Execute(CollisionEvent collisionEvent)
        {
            Entity a = collisionEvent.EntityA;
            Entity b = collisionEvent.EntityB;

            bool aBall = BallLookup.HasComponent(a);
            bool bBall = BallLookup.HasComponent(b);

            bool aBrick = BrickLookup.HasComponent(a);
            bool bBrick = BrickLookup.HasComponent(b);

            if (aBall && bBrick)
            {
                HitBrick(b);
            }
            else if (bBall && aBrick)
            {
                HitBrick(a);
            }
        }

        private void HitBrick(Entity brick)
        {
            var brickData = BrickLookup[brick];
            brickData.hp--;

            BrickLookup[brick] = brickData;

            Score.ValueRW.value += 1;

            if (brickData.hp <= 0)
            {
                ECB.DestroyEntity(brick);
            }
        }
    }
}
