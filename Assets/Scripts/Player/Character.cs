using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Tilemaps;

public class Character : MonoBehaviour {

    public delegate void VictoryDelegate();
    public event VictoryDelegate onVictoryEvent;

    public LayerMask jumpingMask;
    public LayerMask waterMask;
    public float thrust = 450;
    public float speed = 10;
    public float breathChangeRate = 0.01f;

    private Tilemap _waterTiles;
    private Tilemap _groundTiles;

    public Animator animator;

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

    private float _SoundLowpassMax = 22000;
    private float _SoundLowpassMin = 250;
    private string _FreqFilterName = "MyExposedParam";
    public AudioMixer masterMixer;
    private GameManager _gameManager;
    private SpriteRenderer _renderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _stats = GetComponent<PlayerStats>();
        _sounds = GetComponent<CharacterSounds>();

        _renderer = animator.gameObject.GetComponent<SpriteRenderer>();

        _gameManager = StaticReference.GameManager();
        _groundTiles = StaticReference.Ground();
        _waterTiles = StaticReference.Water();


        if (_stats == null)
        {
            throw new MissingComponentException("No PlayerStats found. Player won't take damage. You cheater.");
        }

        if (_sounds == null || masterMixer == null)
        {
            Debug.LogWarning("No CharacterSounds component found. No sounds will play");
        }

        if (_groundTiles == null || _waterTiles == null)
        {
            throw new MissingComponentException("No tilemaps set, you forgot.");
        }
    }

    private void Update()
    {
        if (!AcceptingInput())
        {
            return;
        }

        inWater = IsInWaterCheck();
        current_speed = speed;
        avaible_thrust = thrust;

        if (inWater)
        {
            current_speed *= .5f;
            avaible_thrust *= .3f;
            rb.gravityScale = .5f;
            masterMixer.SetFloat(_FreqFilterName, _SoundLowpassMin);
            _renderer.color = new Color(94f, 155f, 255f, 0.5f);
        }
        else
        {
            rb.gravityScale = 2f;
            masterMixer.SetFloat(_FreqFilterName, _SoundLowpassMax);
            _renderer.color = Color.white;
        }

        // Stuff for double-jump
        bool temp_last_frame_ground_status = isGrounded;
        isGrounded = IsGroundedCheck();

        animator.SetBool("Jumping", !isGrounded);

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

        animator.SetBool("Running", input != 0);
        if (input !=0 && isGrounded && !inWater)
        {
            _sounds.walk();
        }
        if (input > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else if (input < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        float newPosX = transform.position.x + input * current_speed * Time.deltaTime;
        transform.position = new Vector3(newPosX, transform.position.y, -1);
    }

    public void Jump()
    {
        if (CanJump() || inWater)
        {
            _sounds.jump();
            rb.AddForce(transform.up * avaible_thrust);
            if (!inWater)
            {
                jumpsLeft--;
            }
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
            if (!isGrounded)
            {
                jumpsLeft = 2;
            }
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
        Vector3Int cellPosition = _waterTiles.WorldToCell(transform.position);
        TileBase currentTile = _waterTiles.GetTile(cellPosition);
        return currentTile != null;
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

    public void Victory()
    {
        if(onVictoryEvent != null)
        {
            onVictoryEvent.Invoke();
        }
    }

    private bool AcceptingInput()
    {
        return !GameManager.PAUSED_STATES.Contains(_gameManager.gameState);
    }
}
