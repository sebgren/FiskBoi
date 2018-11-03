using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject player;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
        Vector3 newPos = player.transform.position;
        newPos.z = -15f;

        transform.position = Vector3.Lerp(transform.position, newPos, 1f);
    }
}
