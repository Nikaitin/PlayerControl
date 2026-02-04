using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody2D body;
    public float moveSpeed = 5f;
    Vector2 movement;
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        body.MovePosition(body.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
    //I think therefore I am
}
