using UnityEngine;
using System.Collections;

public class asset_deform : MonoBehaviour {
	public bool bHit;
	public bool bReset;
	bool hasAnimation;
	public float deformSpeed = 1.0f;
	public float currentAnimeTime;

	public bool bCausesWaterSplash;
	ParticleSystem psBox;
	ParticleSystem psWaterSplash;
	public ParticleSystem customParticle;

	public float initHealth = 20.0f;
	public float health = 0.0f;
	public float damage = 1.0f; // temporary
	public float force = 10.0f;
	public bool stillAlive = true;

	public enum _assetStates{idle,hit,dead,limbo,respawn}
	public _assetStates _asset_state;


	// Use this for initialization
	void Start () {
		_asset_state = _assetStates.idle;
		animeCheck ();
	}

	void stateControl(){
		if (_asset_state == _assetStates.idle) {
			if(hasAnimation){
				animation[animation.clip.name].speed = 0;
			}
		}
		if (_asset_state == _assetStates.hit) {
			health -= damage;
			if(hasAnimation){
				if(animation[animation.clip.name].normalizedTime < 0.9f){
					animation[animation.clip.name].speed = 5*(deformSpeed);
					if(customParticle == null){
						psBox = Instantiate (Resources.Load("psBoxParticles"),transform.position, transform.rotation) as ParticleSystem;
					}

				}
			}
			else if(customParticle != null){
					psBox = Instantiate (customParticle,transform.position, transform.rotation) as ParticleSystem;
			}
			if (health <= 0.0f) {
				health = 0.0f;
				_asset_state = _assetStates.dead;
			}
		}
		if (_asset_state == _assetStates.dead) {
			gameObject.rigidbody.constraints = RigidbodyConstraints.None;
			rigidbody.AddExplosionForce(force, Vector3.up * force, 10.0f);	
			if(bCausesWaterSplash){
				psWaterSplash = Instantiate(Resources.Load("psWaterSplash"), transform.position, transform.rotation) as ParticleSystem;
			}

			_asset_state = _assetStates.limbo;
		}
		if (_asset_state == _assetStates.limbo) {

			// start timer .... to respawn		
		}
		if (_asset_state == _assetStates.respawn) {
			health = initHealth;
			if(hasAnimation){
				animation.Rewind();
				animation.Play();
				animation[animation.clip.name].speed = 0;
			}
			_asset_state = _assetStates.idle;
		}
	}

	void animeCheck(){
		if (GetComponent<Animation> () != null) {
			hasAnimation = true;
			animation.Play();
			animation[animation.clip.name].speed = 0;
		}
	}
	// Update is called once per frame
	void Update () {

		stateControl ();
	//	hitCheck ();
	//	healthCheck ();
	//	deathCheck ();
	}

//	void healthCheck(){
//
//	}
//	void hitCheck(){
//		if(hasAnimation){
//			if (bHit) {
//				if(animation[animation.clip.name].normalizedTime < 0.9f){
//					animation[animation.clip.name].speed = 5*(deformSpeed);
//					if(customParticle == null){
//						psBox = Instantiate (Resources.Load("psBoxParticles"),transform.position, transform.rotation) as ParticleSystem;
//					}
//			//		else{
//					//	psBox = Instantiate(customParticle, transform.position, transform.rotation) as ParticleSystem;
//			//		}
//			//		psBox.renderer = ParticleSystemRenderMode.Billboard;
//			//	psBox.renderer.material = GetComponentInParent<MeshRenderer>().material;// .renderer.material;
//
//				}
//				else{animation[animation.clip.name].speed = 0;	}
//			}
//			if (!bHit) {
//				animation[animation.clip.name].speed = 0;		
//			}
//			if (bReset) {
//				animation.Rewind();		
//			}
//		}
//		else{
//			if(bHit){
//				health -= damage;
//				if(customParticle != null){
//					psBox = Instantiate (customParticle,transform.position, transform.rotation) as ParticleSystem;
//				}
//			}
//		}
//	}
//
//	void deathCheck(){
//	
//		if (health <= 0.0f) {
//			health = 0.0f;
//			gameObject.rigidbody.constraints = RigidbodyConstraints.None;
//			rigidbody.AddExplosionForce(force, Vector3.up * force, 10.0f);	
//			//	rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
//			//	rigidbody.AddForce(Vector3.up * force);
//			if(stillAlive){
//				if(bCausesWaterSplash){
//					psWaterSplash = Instantiate(Resources.Load("psWaterSplash"), transform.position, transform.rotation) as ParticleSystem;
//				}
//				stillAlive = false;
//			}
//		}
//	}

}
