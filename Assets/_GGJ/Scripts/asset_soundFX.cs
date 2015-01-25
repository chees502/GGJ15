﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class asset_soundFX : MonoBehaviour {

	public List<AudioClip> soundFXs = new List<AudioClip>();

	public int soundIndex = 0;
	public bool bPlaySound;

	public enum _sound_states{idle,begin,playing,end}
	public _sound_states _current_state;
		// Use this for initialization
	void Start () {
		if (GetComponent<AudioSource> () == null) {
			gameObject.AddComponent<AudioSource>();		
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(soundFXs.Count > 0){
			audio.clip = soundFXs [soundIndex];
		}
		if(_current_state == _sound_states.begin || bPlaySound){
			audio.Play ();
			_current_state = _sound_states.playing;
			bPlaySound = false; // for testing purposes
		}
		if (_current_state == _sound_states.playing) {
			if(!audio.isPlaying){
				_current_state = _sound_states.end;
			}		
		}
		if (_current_state == _sound_states.end) {
			_current_state = _sound_states.idle;
		}
	}
}
