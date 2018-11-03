using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolume : MonoBehaviour {

	public TriggerType VolumeType;

	public GameManager gameManager;

	void OnTriggerEnter2D(Collider2D col)
    {
        gameManager.RegisterTrigger(VolumeType, col.gameObject);
    }
}

public enum TriggerType {
	Win,
	Lose
}