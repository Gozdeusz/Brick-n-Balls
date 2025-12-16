using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

public partial struct ShootSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        if (!SystemAPI.TryGetSingleton<BallPrefab>(out var prefab))
            return;

        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach (var (request, requestEntity)
                 in SystemAPI.Query<ShootRequest>().WithEntityAccess())
        {
            Entity ball = ecb.Instantiate(prefab.prefab);

            ecb.SetComponent(ball,
                LocalTransform.FromPosition(float3.zero));

            float speed = SystemAPI.GetComponent<BallSpeed>(ball).value;

            ecb.SetComponent(ball, new PhysicsVelocity
            {
                Linear = new float3(
                    request.direction.x,
                    request.direction.y,
                    0f) * speed,
                Angular = float3.zero
            });

            ecb.DestroyEntity(requestEntity);
        }

        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}