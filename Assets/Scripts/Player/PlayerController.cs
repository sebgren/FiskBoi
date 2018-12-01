using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Vector3 spawnPosition;
    Character character;

    GameManager _gameManager;

    private void Start()
    {
        spawnPosition = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
        character = GetComponent<Character>();

        GameObject manager = GameObject.Find("GameManager");

        if (!manager)
        {
            throw new MissingReferenceException("There is no GameManager in the scene?!?!");
        }
        _gameManager = manager.GetComponent<GameManager>();

    }

    void Update () {

        if (AcceptingInput())
        {

            character.Move(Input.GetAxis("Horizontal"));

            if (Input.GetKeyDown("space"))
            {
                character.Jump();
            }

        }
    }

    public void Respawn() {
        transform.position = spawnPosition;
    }

    private bool AcceptingInput()
    {
        return !GameManager.PAUSED_STATES.Contains(_gameManager.gameState);
    }
}
