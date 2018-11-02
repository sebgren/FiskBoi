using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public LayerMask jumpingMask;
    public LayerMask waterMask;
    public float thrust;
    public float speed;
    private Rigidbody2D rb;
    private Vector3 spawnPosition;
    public float status = 0;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        spawnPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        float current_speed = speed;
        float avaible_thrust = thrust;

        if (InWater()){
            current_speed *= .5f;
            avaible_thrust *= .2f;
            rb.gravityScale = .5f;
            
            status += Time.deltaTime;
        } else {
            rb.gravityScale = 2f;
            status -= Time.deltaTime;
        }

        if (Input.GetKeyDown("space"))
        {
            if (CanJump() || InWater()) {
                rb.AddForce(transform.up * avaible_thrust);
            }
        }

        float newPosX = transform.position.x + Input.GetAxis("Horizontal") * current_speed * Time.deltaTime;
        transform.position = new Vector3(newPosX, transform.position.y);
	}

    bool CanJump() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, jumpingMask);
 
        if (hit.collider != null) {
            return true;
        }

        return false;
    }

    bool InWater(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, .5f, waterMask);

        if (hit.collider != null) {
            Debug.Log("In water");
            return true;
        }

        return false;
    }

    public void Respawn() {
        transform.position = spawnPosition;
    }
}
