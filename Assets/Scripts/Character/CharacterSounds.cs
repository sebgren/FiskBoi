using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour {

    [SerializeField]
    AudioSource _characterSoundSource;

    [SerializeField]
    AudioClip _jump;

	// Use this for initialization
	void Start () {
		_characterSoundSource = GetComponent<AudioSource>();

        if (_characterSoundSource == null)
        {
            throw new MissingComponentException("Missing AudioSource");
        }

        if (_jump == null)
        {
            Debug.LogWarning("No Jump Sound found. No sounds will play when jumping");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void jump()
    {
        _characterSoundSource.PlayOneShot(_jump);
    }
}
