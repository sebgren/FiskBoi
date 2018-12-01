using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour {

    [SerializeField]
    AudioSource _characterSoundSource;

    [SerializeField]
    AudioClip _jump;

    [SerializeField]
    AudioClip _step1;
    [SerializeField]
    AudioClip _step2;

    float _timeSinceLastWalk = 0;

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
        _timeSinceLastWalk += Time.deltaTime;
  
    }

    public void jump()
    {
        _characterSoundSource.PlayOneShot(_jump);
    }

    public void walk()
    {

        if (_timeSinceLastWalk < 0.2)
        {
            return;
        }
        _timeSinceLastWalk = 0;
        float val = Random.Range(0, 10);
        if (val < 5)
        {
            _characterSoundSource.PlayOneShot(_step1);
        } else
        {
            _characterSoundSource.PlayOneShot(_step2);
        }
    }
}
