using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Vector3 spawnPosition;
    Character character;
    Rigidbody2D _rigidbody2D;

    GameManager _gameManager;

    private void Start()
    {
        spawnPosition = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
        character = GetComponent<Character>();

        _gameManager = StaticReference.GameManager();
        _rigidbody2D = GetComponent<Rigidbody2D>();

    }

    void Update () {

        if (AcceptingInput())
        {

            character.Move(Input.GetAxis("Horizontal"));

            if (Input.GetButtonDown("Jump"))
            {
                character.Jump();
            }

            if (Input.GetButtonDown("reset"))
            {
                _gameManager.respawn();
            }

        }
    }

    public void Respawn() {
        transform.position = spawnPosition;
        _rigidbody2D.velocity = Vector3.zero;
    }

    private bool AcceptingInput()
    {
        return !GameManager.PAUSED_STATES.Contains(_gameManager.gameState);
    }
}
