using UnityEngine;
using System.Collections;


[RequireComponent(typeof(objectDetect))]
[RequireComponent(typeof(EntityRespawn))]
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
	public bool stillAlive = true;

	public enum _assetStates{idle,hit,dead,limbo,respawn}
	public _assetStates _asset_state;

    private EntityRespawn respawner;

	public delegate void BasicEvent();
	public event BasicEvent OnObjectDamage;
	public event BasicEvent OnObjectDestroy;
	public event BasicEvent OnObjectRespawn;

	void TriggerObjectDamage(){
		if (OnObjectDamage != null) {
			OnObjectDamage();		
		}
	}

	void TriggerObjectDestroy(){
		if (OnObjectDestroy != null) {
			OnObjectDestroy();		
		}
	}

	void TriggerObjectRespawn(){
		if (OnObjectRespawn != null) {
			OnObjectRespawn();		
		}
	}

    void Awake() {
        respawner = GetComponent<EntityRespawn>();
        if (respawner == null) {
            Debug.LogWarning(string.Format("[asset_deform]: No EntityRespawn script attached to gameobject {0}", gameObject.name));
        }
        respawner.OnEntityDestroyed += OnAssetDestroy;
        respawner.OnEntityRespawned += OnAssetRespawn;
    }

	// Use this for initialization
	void Start () {
		_asset_state = _assetStates.respawn;
		animeCheck ();
	}

    void OnAssetDestroy() {
        //_asset_state = _assetStates.limbo;
        TriggerObjectDestroy();
    }

    void OnAssetRespawn() {
        _asset_state = _assetStates.respawn;
        TriggerObjectRespawn();
    }

	void stateControl(){
		if (_asset_state == _assetStates.idle) {
			if(hasAnimation){
				animation[animation.clip.name].speed = 0;
			}
		}
		if (_asset_state == _assetStates.hit) {
            _asset_state = _assetStates.idle;

            DamageAsset(damage);
			
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
		}
		if (_asset_state == _assetStates.dead) {
			gameObject.rigidbody.constraints = RigidbodyConstraints.None;
            //rigidbody.AddForce(Vector3.up * 100, ForceMode.VelocityChange);
			if(bCausesWaterSplash){
				psWaterSplash = Instantiate(Resources.Load("psWaterSplash"), transform.position, transform.rotation) as ParticleSystem;
			}
            _asset_state = _assetStates.limbo;
            respawner.Destroy(2.0f, 3.0f);
		}
		if (_asset_state == _assetStates.limbo) {

			// Does nothing		
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

    public void DamageAsset(float damage) {
        health -= Mathf.Max(0, damage);

        if (damage > 0) {
            TriggerObjectDamage();
        }

        if (health <= 0.0f) {
            health = 0.0f;
            _asset_state = _assetStates.dead;
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
