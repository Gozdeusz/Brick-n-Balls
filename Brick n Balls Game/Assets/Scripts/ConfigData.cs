using Unity.Entities;
using UnityEngine;

public struct BallPrefab : IComponentData
{
    public Entity prefab;
}

public class ConfigAuthoring : MonoBehaviour
{
    public GameObject ballPrefab;
}

public class ConfigBaker : Baker<ConfigAuthoring>
{
    public override void Bake(ConfigAuthoring authoring)
    {
        Entity configEntity = GetEntity(TransformUsageFlags.None);

        Entity ballEntity = GetEntity(authoring.ballPrefab, TransformUsageFlags.Dynamic);

        AddComponent(configEntity, new BallPrefab
        {
            prefab = ballEntity
        });
    }
}