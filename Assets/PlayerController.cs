using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Vector3 spawnPosition;
    CharacterController character;

    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void Update () {

        character.Move(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown("space"))
        {
            character.Jump();
        }
    }

    public void Respawn() {
        transform.position = spawnPosition;
    }
}
