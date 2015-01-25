using UnityEngine;
using System.Collections;

public class asset_deform : MonoBehaviour {
	public bool bHit;
	public bool bReset;
	bool hasAnimation;
	public float deformSpeed = 1.0f;
	public float currentAnimeTime;

	ParticleSystem psBox;
	ParticleSystem psWaterSplash;

	public float health = 20.0f;
	public float damage = 1.0f; // temporary
	public float force = 10.0f;
	// Use this for initialization
	void Start () {
		if (GetComponent<Animation> () != null) {
			hasAnimation = true;
			animation.Play();
			animation[animation.clip.name].speed = 0;
		}

	}
	
	// Update is called once per frame
	void Update () {
		hitCheck ();
		healthCheck ();
		deathCheck ();
	}

	void healthCheck(){

	}
	void hitCheck(){
		if(hasAnimation){
			if (bHit) {
				if(animation[animation.clip.name].normalizedTime < 0.9f){
					animation[animation.clip.name].speed = 5*(deformSpeed);
					Instantiate (Resources.Load("psBoxParticles"),transform.position, transform.rotation);
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
		else{
			if(bHit){
				health -= damage;
				
			}
		}
	}

	void deathCheck(){
		if (health <= 0.0f) {
			health = 0.0f;
			gameObject.rigidbody.constraints = RigidbodyConstraints.None;
			rigidbody.AddExplosionForce(force, Vector3.up * force, 10.0f);	
			//	rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
			//	rigidbody.AddForce(Vector3.up * force);
			psWaterSplash = Instantiate(Resources.Load("psWaterSplash"), transform.position, transform.rotation) as ParticleSystem;
		}
	}

}
