using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform arrowTransform;

    private EntityManager entityManager;

    private float2 aimDirection = new float2(0, 1);

    private void Awake()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (input.sqrMagnitude < 0.01f)
            return;

        aimDirection = math.normalize(new float2(input.x, input.y));

        float angle = math.degrees(math.atan2(aimDirection.y, aimDirection.x));
        arrowTransform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        Entity request = entityManager.CreateEntity();
        entityManager.AddComponentData(request, new ShootRequest
        {
            direction = aimDirection
        });
    }

}
