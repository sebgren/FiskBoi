using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnClick : MonoBehaviour
{
    GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.Find("GameManager");

        if (!manager)
        {
            throw new MissingReferenceException("There is no GameManager in the scene?!?!");
        }
        _gameManager = manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRespawn()
    {
        _gameManager.respawn();
    }
}
