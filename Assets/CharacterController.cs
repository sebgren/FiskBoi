using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public LayerMask jumpingMask;
    public LayerMask waterMask;
    public float thrust;
    public float speed;
    private Rigidbody2D rb;
    public float status = 0;

    private float currentSpeed;
    public float CurrentSpeed
    {
        get
        {
            if (currentSpeed < 0)
                return currentSpeed * -1;
            return currentSpeed;
        }
    }

    private bool inWater;
    private float current_speed;
    private float avaible_thrust;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inWater = IsInWaterCheck();
        current_speed = speed;
        avaible_thrust = thrust;

        if (inWater)
        {
            current_speed *= .5f;
            avaible_thrust *= .2f;
            rb.gravityScale = .5f;

            status += Time.deltaTime;
        }
        else
        {
            rb.gravityScale = 2f;
            status -= Time.deltaTime;
        }
    }

    public void Move(float input)
    {
        float newPosX = transform.position.x + input * current_speed * Time.deltaTime;
        transform.position = new Vector3(newPosX, transform.position.y);
    }

    public void Jump()
    {
        if (CanJump() || inWater)
        {
            Debug.Log("Jump");

            rb.AddForce(transform.up * avaible_thrust);
        }
    }

    bool CanJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, jumpingMask);

        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    bool IsInWaterCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, .5f, waterMask);

        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
