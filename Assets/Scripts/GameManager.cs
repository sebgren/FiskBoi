using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public void RegisterTrigger(TriggerType e_type, GameObject player) {
		if (e_type == TriggerType.Lose) { 
			Debug.Log("You lost!"); 
			player.GetComponent<PlayerController>().Respawn();
		}
		if (e_type == TriggerType.Win) { Debug.Log("You win!"); }
	}
}
