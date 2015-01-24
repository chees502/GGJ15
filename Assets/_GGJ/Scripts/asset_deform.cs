using UnityEngine;
using System.Collections;

public class asset_deform : MonoBehaviour {
	public bool bHit;
	public bool bReset;
	public float deformSpeed = 1.0f;
	public float currentAnimeTime;
	ParticleSystem psBox;
	// Use this for initialization
	void Start () {
		animation.Play();
		animation[animation.clip.name].speed = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (bHit) {
			if(animation[animation.clip.name].normalizedTime < 0.9f){
				animation[animation.clip.name].speed = 5*(deformSpeed);
				psBox = Instantiate (Resources.Load("psBoxParticles"),transform.position, transform.rotation) as ParticleSystem;
			}
			else{animation[animation.clip.name].speed = 0;	}
		}
		if (!bHit) {
			animation[animation.clip.name].speed = 0;		
		}
		if (bReset) {
			animation.Rewind();		
		}
	}

}
