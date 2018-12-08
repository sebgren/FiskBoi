using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    public int? indexOverride = null;

	public void LoadByIndex(int sceneIndex)
    {
        int indexToLoad = indexOverride == null ? sceneIndex : indexOverride.Value;
        SceneManager.LoadScene(sceneIndex);
    }

    // Load main menu

    // Load level (int level_nr)
}
