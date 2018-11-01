using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    private float jumpTimer;
    float newPosX;
    float newPosY;
    public Rigidbody2D rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space"))
        {
            transform.Translate(Vector3.up * 50 * Time.deltaTime, Space.World);



        }

        newPosX = transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.position = new Vector3(newPosX, transform.position.y);
	}
}
