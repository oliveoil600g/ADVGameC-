using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float moveSpeed = 5f;

    Vector2 moveValue;

    public void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        var current = rb.linearVelocity;

        rb.linearVelocity = new Vector3(
            moveValue.x * moveSpeed,
            current.y,
            current.z
        );
        GameManager.Instance.PlayerSpeed = rb.linearVelocity;
    }
}
