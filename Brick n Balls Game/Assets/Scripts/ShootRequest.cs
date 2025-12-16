using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct ShootRequest : IComponentData
{
    public float2 direction;
}
