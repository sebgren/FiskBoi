using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject _player;

    private PlayerStats _PlayerStats;

    public void Start()
    {
        if (_player == null)
        {
            throw new MissingComponentException("Player is not set!");
        }

        _PlayerStats = _player.GetComponent<PlayerStats>();

        // TODO Add death reason
        _PlayerStats.deathEvent += (target, reason) =>
        {
            gameOver();
        };

    }


    // TODO Move this trigger event to the player controller
    public void RegisterTrigger(TriggerType e_type, GameObject player) {
		if (e_type == TriggerType.Lose) { 
			player.GetComponent<PlayerStats>().die();
		}
		if (e_type == TriggerType.Win) { Debug.Log("You win!"); }
	}

    private void gameOver()
    {
        Debug.Log("You Died!");
        _player.GetComponent<PlayerController>().Respawn();
    }

}
