using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Vignetting))]
[RequireComponent(typeof(TwirlEffect))]
[RequireComponent(typeof(BloomAndLensFlares))]
[RequireComponent(typeof(ColorCorrectionCurves))]
public class abberationBend : MonoBehaviour {
    static private List<abberationBend> _instances;
    static public List<abberationBend> Instances {
        get {
            if (_instances == null) {
                _instances = new List<abberationBend>();
            }
            return _instances; 
        }
    }

    public Vignetting Vignet;
    public TwirlEffect Twirl;
    public ColorCorrectionCurves CCEffect;
    public BloomAndLensFlares Bloom;
    public float TrippingTill;
    public float TrippingPower = 0;
    public bool Tripping = false;
    public bool Hot = false;
    public float HotTill;
    public bool callScript = false;

	// Use this for initialization
    public static void StartAllTripping(float time)
    {
        Debug.Log("Start All Tripping");
        foreach (abberationBend bend in Instances) {
            bend.StartTrip(time);
        }
    }
    public static void StartAllHot(float time)
    {
        Debug.Log("Start All Hot");
        foreach (abberationBend bend in Instances) {
            bend.StartHot(time);
        }
    }
    public static void CancelAllTripping() 
    {
        Debug.Log("Cancel All Tripping");
        foreach (abberationBend bend in Instances) {
            bend.CancelTrip();
        }
    }
    public static void CancelAllHot() 
    {
        Debug.Log("Cancel All Hot");
        foreach (abberationBend bend in Instances) {
            bend.CancelHot();
        }
    }

    void Awake() {
        if (!Instances.Contains(this)) {
            Instances.Add(this);
        }
    }

    void OnDestroy() {
        Instances.Remove(this);
        if (Instances.Count == 0) {
            _instances = null;
        }
    }

	void Start () {
        Vignet = gameObject.GetComponent<Vignetting>();
        Vignet.enabled = false;
        Twirl = gameObject.GetComponent<TwirlEffect>();
        Twirl.enabled = false;
        CCEffect = gameObject.GetComponent<ColorCorrectionCurves>();
        CCEffect.enabled = false;
        Bloom = gameObject.GetComponent<BloomAndLensFlares>();
        Bloom.enabled = false;
	}

    public void StartTrip(float duration) {
        if (Tripping == true) return;

        Vignet.enabled = true;
        Twirl.enabled = true;
        TrippingTill = Time.time + duration;
        Tripping = true;
    }

    public void CancelTrip() {
        TrippingPower = 0;
        Vignet.enabled = false;
        Twirl.enabled = false;
        Tripping = false;
    }

    public void StartHot(float duration) {
        if (Hot == true) return;

        CCEffect.enabled = true;
        Bloom.enabled = true;
        HotTill = Time.time + duration;
        Hot = true;
    }

    public void CancelHot() {
        Hot = false;
        CCEffect.enabled = false;
        Bloom.enabled = false;
    }

	// Update is called once per frame
	void Update () {
        if (callScript)
        {
            callScript = false;
            //callTripping(10);
            StartHot(5);
        }
        if (Tripping)
        {
            if (TrippingTill > Time.time)
            {
                TrippingPower = Mathf.Min(1, TrippingPower + Time.deltaTime);
            }
            else
            {
                TrippingPower = Mathf.Max(0, TrippingPower - Time.deltaTime);
            }
            if(TrippingPower==0){
                Vignet.enabled = false;
                Twirl.enabled = false;
                Tripping=false;
            }
        }
        if (Hot)
        {
            Bloom.bloomIntensity = 10 + Mathf.Sin(Time.time * 10) * 5;
            if (HotTill < Time.time)
            {
                Hot = false;
                CCEffect.enabled = false;
                Bloom.enabled = false;

            }
        }
        Vignet.chromaticAberration = Mathf.Sin(Time.time * 0.5f) * 200*TrippingPower;
        Twirl.angle = Mathf.Sin(Time.time * 0.8f) * 10 * TrippingPower;
	}
}
