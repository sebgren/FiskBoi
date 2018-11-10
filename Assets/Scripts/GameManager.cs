using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject _player;

    private PlayerStats _PlayerStats;

    [SerializeField]
    private GameObject _deathMenu;

    private GameState _gameState = GameState.STARTED;

    public delegate void GameStateChangeDelegate(GameState newState, GameState oldState);
    public event GameStateChangeDelegate onGameStateChange;

    public void Start()
    {
        if (_player == null)
        {
            throw new MissingComponentException("Player is not set!");
        }

        if (_deathMenu == null)
        {
            throw new MissingComponentException("Death Menu is not set!");
        }

        _PlayerStats = _player.GetComponent<PlayerStats>();

        _PlayerStats.deathEvent += (target, reason) =>
        {
            gameOver(reason);
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
        gameOver(DeathReason.UNKNOWN);
    }

    private void gameOver(DeathReason reason)
    {
        _deathMenu.SetActive(true);
        this.gameState = GameState.PAUSED;
    }

    public void respawn()
    {
        _player.GetComponent<PlayerController>().Respawn();
        _PlayerStats.reset();
        _deathMenu.SetActive(false);
        this.gameState = GameState.STARTED;
    }

    public enum GameState
    {
        UNKOWN,
        STARTED,
        PAUSED
    }

    public static HashSet<GameState> PAUSED_STATES = new HashSet<GameState> { GameState.UNKOWN, GameState.PAUSED };


    public GameState gameState
    {
        get
        {
            return _gameState;
        }

        set
        {
            GameState oldState = this.gameState;
            _gameState = value;
            if (onGameStateChange != null)
            {
                onGameStateChange.Invoke(gameState, oldState);
            }
        }
    }

}
