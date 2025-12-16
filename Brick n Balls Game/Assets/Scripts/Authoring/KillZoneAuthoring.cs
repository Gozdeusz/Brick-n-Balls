using Unity.Entities;
using UnityEngine;

public struct KillZoneTag : IComponentData { }

public class KillZoneAuthoring : MonoBehaviour
{
}

public class KillZoneBaker : Baker<KillZoneAuthoring>
{
    public override void Bake(KillZoneAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.None);
        AddComponent<KillZoneTag>(entity);
    }
}
