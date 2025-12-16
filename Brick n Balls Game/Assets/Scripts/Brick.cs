using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private int hp = 1;
    private int point = 1;

    public static event Action<int> OnBrickDestroyed;
    private void OnDestroy()
    {
        OnBrickDestroyed?.Invoke(point);
    }
}
