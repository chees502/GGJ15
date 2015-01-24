using UnityEngine;
using System.Collections;

public class PSEmissionDecay : MonoBehaviour {

	public float killTime = 2.0f;
	// Use this for initialization
	void Start () {
		StartCoroutine ("killParticle");
		transform.rotation = Quaternion.Euler (270.0f, 0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (particleSystem.emissionRate <= 0.0f) {
			Destroy(gameObject, 1.0f);		
		}
	}

	IEnumerator killParticle(){
		yield return new WaitForSeconds (killTime);
		particleSystem.emissionRate = 0.0f;
	}
}
