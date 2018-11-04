using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Vector3 spawnPosition;
    Character character;

    private void Start()
    {
        character = GetComponent<Character>();
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
