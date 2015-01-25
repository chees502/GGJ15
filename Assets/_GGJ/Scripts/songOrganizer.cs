using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class songOrganizer : MonoBehaviour {

	public List<AudioClip> soundTracks = new List<AudioClip>();
	public enum _soundTrackStates{main, weed, meth, lsd, fire}
	static public _soundTrackStates _stState;

	AudioClip currentTrack;

	public int testInt = 0;
	// Use this for initialization

	void Start () {
		_stState = _soundTrackStates.main;
	}
	
	// Update is called once per frame
	void Update () {
		PlaySoundTrack ();
	}

	void PlaySoundTrack(){
		if (_stState == _soundTrackStates.main) {
			audio.clip = soundTracks[testInt];
		}
		if (_stState == _soundTrackStates.weed) {
			audio.clip = soundTracks[testInt];
		}
		if (_stState == _soundTrackStates.meth) {
			audio.clip = soundTracks[testInt];
		}
		if (_stState == _soundTrackStates.lsd) {
			audio.clip = soundTracks[testInt];
		}
		if(_stState == _soundTrackStates.fire){
			audio.clip = soundTracks[testInt];
		}
		if(!audio.isPlaying){
			audio.Play ();
		}
	}
}
