using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public LayerMask jumpingMask;
    public LayerMask waterMask;
    public float thrust = 450;
    public float speed = 10;
    public float breathChangeRate = 0.01f;

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

    private Rigidbody2D rb;
    private int jumpsLeft = 2;
    private bool inWater;
    private bool isGrounded;
    private float current_speed;
    private float avaible_thrust;

    private PlayerStats _stats;
    private CharacterSounds _sounds;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _stats = GetComponent<PlayerStats>();
        _sounds = GetComponent<CharacterSounds>();

        if (_stats == null)
        {
            throw new MissingComponentException("No PlayerStats found. Player won't take damage. You cheater.");
        }

        if (_sounds == null)
        {
            Debug.LogWarning("No CharacterSounds component found. No sounds will play");
        }
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
        }
        else
        {
            rb.gravityScale = 2f;
        }

        // Stuff for double-jump
        bool temp_last_frame_ground_status = isGrounded;
        isGrounded = IsGroundedCheck();

        // If player was not grounded last frame but is now, reset double jump
        if (!temp_last_frame_ground_status && isGrounded) jumpsLeft = 2;

        UpdateBreath();
    }

    /// <summary>
    /// Move the character on the x-axis
    /// </summary>
    /// <param name="input">Typically a input-axis value</param>
    public void Move(float input)
    {
        float newPosX = transform.position.x + input * current_speed * Time.deltaTime;
        transform.position = new Vector3(newPosX, transform.position.y);
    }

    public void Jump()
    {
        if (CanJump() || inWater)
        {
            _sounds.jump();
            rb.AddForce(transform.up * avaible_thrust);
            jumpsLeft--;
        }
    }

    /// <summary>
    /// Checks if a jump is possible
    /// </summary>
    /// <returns>True if a jump can be done otherwise false</returns>
    bool CanJump()
    {
        return isGrounded || jumpsLeft > 0;
    }

    /// <summary>
    ///     Checks if the player is grounded by raycasting
    /// </summary>
    /// <returns>True if grounded otherwise false</returns>
    bool IsGroundedCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, jumpingMask);

        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Checks if the player is grounded by raycasting
    /// </summary>
    /// <returns>True if in water otherwise false</returns>
    bool IsInWaterCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, .5f, waterMask);

        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    private void UpdateBreath()
    {
        if (inWater)
        {
            _stats.subBreathOrDie(breathChangeRate * Time.deltaTime);
        } else
        {
            _stats.addBreathOrDie(breathChangeRate * Time.deltaTime);
        }
    }
}
