using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class VictorySCreen : MonoBehaviour
{

    GameManager manager;

    [SerializeField]
    Text hs;
    [SerializeField]
    Text score;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        manager.onGameStateChange += (GameManager.GameState newState, GameManager.GameState oldState) =>
        {
            hs.text = "HighScore " + Math.Round(PlayerPrefs.GetFloat("HS" + SceneManager.GetActiveScene().buildIndex), 2).ToString();
        };

        
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score " + Math.Round(manager._timer, 2).ToString();
    }
}
