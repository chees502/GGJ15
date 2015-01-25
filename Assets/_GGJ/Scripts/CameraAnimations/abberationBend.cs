using UnityEngine;
using System.Collections;

public class abberationBend : MonoBehaviour {
    static private abberationBend _instance;
    static public abberationBend Instance {
        get { return _instance; }
    }

    public static Vignetting Vignet;
    public static TwirlEffect Twirl;
    public static ColorCorrectionCurves CCEffect;
    public static BloomAndLensFlares Bloom;
    public static float TrippingTill;
    public static float TrippingPower = 0;
    public static bool Tripping = false;
    public static bool Hot = false;
    public static float HotTill;
    public bool callScript = false;
	// Use this for initialization
    public static void callTripping(float time)
    {
        Vignet.enabled = true;
        Twirl.enabled = true;
        TrippingTill = Time.time + time;
        Tripping = true;
    }
    public static void callHot(float time)
    {
        CCEffect.enabled = true;
        Bloom.enabled = true;
        HotTill = Time.time + time;
        Hot = true;
    }
    public static void cancelTripping() 
    {
        TrippingPower = 0;
        Vignet.enabled = false;
        Twirl.enabled = false;
        Tripping = false;
    }
    public static void cancelHot() 
    {

    }

    void Awake() {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != this) {
            Debug.LogWarning("[abberationBend]: More then one instance of the script in the scene");
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
        Vignet.enabled = true;
        Twirl.enabled = true;
        TrippingTill = Time.time + duration/2f;
        Tripping = true;
    }

    public void CancelTrip() {
        
    }

    public void StartHot(float duration) {
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
            callHot(5);
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
