using Unity.Entities;
using Unity.Transforms;

public partial struct KillZoneSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (zoneTransform, zoneEntity)
                 in SystemAPI.Query<RefRO<LocalTransform>>()
                     .WithAll<KillZoneTag>()
                     .WithEntityAccess())
        {
            float limitY = zoneTransform.ValueRO.Position.y;

            foreach (var (ballTransform, ballEntity)
                     in SystemAPI.Query<RefRO<LocalTransform>>()
                         .WithAll<BallTag>()
                         .WithEntityAccess())
            {
                if (ballTransform.ValueRO.Position.y < limitY)
                {
                    state.EntityManager.DestroyEntity(ballEntity);
                }
            }
        }
    }
}
