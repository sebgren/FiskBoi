using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject _player;

    private PlayerStats _PlayerStats;
    private Character _PlayerCharacter;

    [SerializeField]
    private GameObject _deathMenu;
    [SerializeField]
    private GameObject _victoryMenu;

    private GameState _gameState = GameState.STARTED;

    public delegate void GameStateChangeDelegate(GameState newState, GameState oldState);
    public event GameStateChangeDelegate onGameStateChange;

    public float _timer;

    public void Start()
    {
        if (_player == null)
        {
            throw new MissingComponentException("Player is not set!");
        }

        if (_deathMenu == null || _victoryMenu == null)
        {
            throw new MissingComponentException("Menus are not set!");
        }

        _PlayerStats = _player.GetComponent<PlayerStats>();
        _PlayerCharacter = _player.GetComponent<Character>();

        _PlayerCharacter.onVictoryEvent += () =>
        {
            gameState = GameState.PAUSED;
            _victoryMenu.SetActive(true);
            float hs = PlayerPrefs.GetFloat("HS" + SceneManager.GetActiveScene().buildIndex);
            print(hs);
            if (hs > _timer || hs == 0)
            {
                PlayerPrefs.SetFloat("HS" + SceneManager.GetActiveScene().buildIndex, _timer);
            }
        };

        _PlayerStats.deathEvent += (target, reason) =>
        {
            gameOver(reason);
        };
        start();
    }

    public void Update()
    {
        if (_gameState == GameState.STARTED)
        {
            _timer += Time.deltaTime;
        }
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
        start();
    }

    public void start()
    {
        _victoryMenu.SetActive(false);
        this.gameState = GameState.STARTED;
        _timer = 0;
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
