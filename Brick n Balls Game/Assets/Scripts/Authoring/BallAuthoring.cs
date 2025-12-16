using Unity.Entities;
using Unity.Physics;
using UnityEngine;

public struct BallTag : IComponentData { }

public struct BallSpeed : IComponentData
{
    public float value;
}

public class BallAuthoring : MonoBehaviour
{
    [Header("Ball settings")]
    public float speed = 8f;
}

public class BallBaker : Baker<BallAuthoring>
{
    public override void Bake(BallAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent<BallTag>(entity);

        AddComponent(entity, new BallSpeed
        {
            value = authoring.speed
        });

        AddComponent(entity, PhysicsVelocity.Zero);

        AddComponent(entity, PhysicsMass.CreateDynamic(
            MassProperties.UnitSphere, 1f));

        AddComponent(entity, new PhysicsDamping
        {
            Linear = 0f,
            Angular = 0f
        });
    }
}
