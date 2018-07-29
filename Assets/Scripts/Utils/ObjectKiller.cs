using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectKiller : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio) {
            audio.Play();
            Destroy(gameObject, audio.clip.length);
        }
	}
}
