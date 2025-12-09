using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ScreenBounds screenBounds;

    public int speed = 5;
    public int rotationSpeed = 5;
    public int maxSpeed = 10; 
    public Rigidbody2D rb;

    void Update()
    {
        RotateTowardsMouse();
    }
    private void FixedUpdate()
    {
        if (screenBounds.AmIOutOfBounds(this.transform.position))
        {
            Vector2 newPosition = screenBounds.CalculateWrappedPosition(this.transform.position);
            transform.position = newPosition;
        }
        else
        {
            Movement();
        }
    }
    private void Movement()
    {
        float hori = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector2 vector2 = new Vector2(hori * speed, ver * speed);
        rb.AddForce(vector2);

        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    private void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float smoothAngle = Mathf.LerpAngle(rb.rotation, targetAngle + 90, Time.deltaTime * rotationSpeed);
        rb.rotation = smoothAngle;
    }
}
